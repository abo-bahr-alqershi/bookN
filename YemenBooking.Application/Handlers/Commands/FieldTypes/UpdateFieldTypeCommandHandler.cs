using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YemenBooking.Application.Commands.FieldTypes;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces.Repositories;
using Unit = MediatR.Unit;
using System.Text.Json;

namespace YemenBooking.Application.Handlers.Commands.FieldTypes;

/// <summary>
/// معالج أمر تحديث نوع الحقل
/// Update field type command handler
/// 
/// يقوم بتحديث نوع حقل موجود ويشمل:
/// - التحقق من صحة البيانات المدخلة
/// - التحقق من وجود نوع الحقل
/// - التحقق من صلاحيات المستخدم (مسؤول فقط)
/// - التحقق من قواعد العمل
/// - تحديث نوع الحقل
/// - إبطال Cache
/// 
/// Updates existing field type and includes:
/// - Input data validation
/// - Field type existence validation
/// - User authorization validation (Admin only)
/// - Business rules validation
/// - Field type update
/// - Cache invalidation
/// </summary>
public class UpdateFieldTypeCommandHandler : IRequestHandler<UpdateFieldTypeCommand, ResultDto<Unit>>
{
    private readonly IFieldTypeRepository _fieldTypeRepository;
    private readonly IUnitTypeFieldRepository _unitTypeFieldRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidationService _validationService;
    private readonly IAuditService _auditService;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateFieldTypeCommandHandler> _logger;

    public UpdateFieldTypeCommandHandler(
        IFieldTypeRepository fieldTypeRepository,
        IUnitTypeFieldRepository unitTypeFieldRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IValidationService validationService,
        IAuditService auditService,
        IEventPublisher eventPublisher,
        ILogger<UpdateFieldTypeCommandHandler> logger)
    {
        _fieldTypeRepository = fieldTypeRepository;
        _unitTypeFieldRepository = unitTypeFieldRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _validationService = validationService;
        _auditService = auditService;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    /// <summary>
    /// معالجة أمر تحديث نوع الحقل
    /// Handle update field type command
    /// </summary>
    /// <param name="request">طلب تحديث نوع الحقل / Update field type request</param>
    /// <param name="cancellationToken">رمز الإلغاء / Cancellation token</param>
    /// <returns>نتيجة العملية / Operation result</returns>
    public async Task<ResultDto<Unit>> Handle(UpdateFieldTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("بدء معالجة أمر تحديث نوع الحقل: {FieldTypeId}", request.FieldTypeId);

            // الخطوة 1: التحقق من صحة البيانات المدخلة
            var inputValidationResult = await ValidateInputAsync(request, cancellationToken);
            if (!inputValidationResult.Success)
            {
                return inputValidationResult;
            }

            // الخطوة 2: التحقق من وجود نوع الحقل
            var fieldTypeId = request.FieldTypeId;
            var existingFieldType = await _fieldTypeRepository.GetFieldTypeByIdAsync(fieldTypeId, cancellationToken);
            if (existingFieldType == null)
            {
                _logger.LogWarning("نوع الحقل غير موجود: {FieldTypeId}", request.FieldTypeId);
                return ResultDto<Unit>.Failure("نوع الحقل غير موجود");
            }

            // الخطوة 3: التحقق من صلاحيات المستخدم
            var authorizationValidation = await ValidateAuthorizationAsync(cancellationToken);
            if (!authorizationValidation.Success)
            {
                return authorizationValidation;
            }

            // الخطوة 4: التحقق من قواعد العمل
            var businessRulesValidation = await ValidateBusinessRulesAsync(request, existingFieldType, cancellationToken);
            if (!businessRulesValidation.Success)
            {
                return businessRulesValidation;
            }

            // الخطوة 5: حفظ القيم الأصلية للمراجعة
            var originalValues = new
            {
                existingFieldType.Name,
                existingFieldType.DisplayName,
                existingFieldType.ValidationRules,
                existingFieldType.IsActive
            };

            // الخطوة 6: تحديث نوع الحقل
            existingFieldType.Name = request.Name;
            existingFieldType.DisplayName = request.DisplayName;
            existingFieldType.ValidationRules = JsonConvert.SerializeObject(request.ValidationRules ?? new System.Collections.Generic.Dictionary<string, object>());
            existingFieldType.IsActive = request.IsActive;
            existingFieldType.UpdatedAt = DateTime.UtcNow;
            existingFieldType.UpdatedBy = _currentUserService.UserId;

            var updatedFieldType = await _fieldTypeRepository.UpdateFieldTypeAsync(existingFieldType, cancellationToken);

            // الخطوة 7: تسجيل العملية في سجل التدقيق
            await _auditService.LogActivityAsync(
                "FieldType",
                updatedFieldType.Id.ToString(),
                "Update",
                $"تم تحديث نوع الحقل: {updatedFieldType.Name}",
                originalValues,
                updatedFieldType,
                cancellationToken);

            // الخطوة 8: نشر الحدث
            await _eventPublisher.PublishEventAsync(new FieldTypeUpdatedEvent
            {
                FieldTypeId = updatedFieldType.Id,
                Name = updatedFieldType.Name,
                DisplayName = updatedFieldType.DisplayName,
                UpdatedBy = _currentUserService.UserId,
                UpdatedAt = updatedFieldType.UpdatedAt
            }, cancellationToken);

            _logger.LogInformation("تم تحديث نوع الحقل بنجاح: {FieldTypeId}", updatedFieldType.Id);

            return ResultDto<Unit>.Ok(
                Unit.Value,
                "تم تحديث نوع الحقل بنجاح"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في تحديث نوع الحقل: {FieldTypeId}", request.FieldTypeId);
            return ResultDto<Unit>.Failure("حدث خطأ أثناء تحديث نوع الحقل");
        }
    }

    /// <summary>
    /// التحقق من صحة البيانات المدخلة
    /// Validate input data
    /// </summary>
    private async Task<ResultDto<Unit>> ValidateInputAsync(UpdateFieldTypeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validationService.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors);
            _logger.LogWarning("بيانات غير صحيحة لتحديث نوع الحقل: {Errors}", errors);
            return ResultDto<Unit>.Failure($"بيانات غير صحيحة: {errors}");
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }

    /// <summary>
    /// التحقق من صلاحيات المستخدم
    /// Validate user authorization
    /// </summary>
    private Task<ResultDto<Unit>> ValidateAuthorizationAsync(CancellationToken cancellationToken)
    {
        if (_currentUserService.Role != "Admin")
        {
            _logger.LogWarning("المستخدم {UserId} لا يملك صلاحية تحديث أنواع الحقول", _currentUserService.UserId);
            return Task.FromResult(ResultDto<Unit>.Failure("غير مصرح لك بتحديث أنواع الحقول"));
        }

        return Task.FromResult(ResultDto<Unit>.Ok(Unit.Value));
    }

    /// <summary>
    /// التحقق من قواعد العمل
    /// Validate business rules
    /// </summary>
    private async Task<ResultDto<Unit>> ValidateBusinessRulesAsync(UpdateFieldTypeCommand request, FieldType existingFieldType, CancellationToken cancellationToken)
    {
        // التحقق من عدم تعديل أنواع الحقول الأساسية للنظام
        var systemFieldTypes = new[] { "text", "number", "boolean", "date", "select", "multi_select", "file", "textarea" };
        if (systemFieldTypes.Contains(existingFieldType.Name.ToLower()) && 
            !existingFieldType.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("محاولة تعديل نوع حقل أساسي للنظام: {Name}", existingFieldType.Name);
            return ResultDto<Unit>.Failure("لا يمكن تعديل أنواع الحقول الأساسية للنظام");
        }

        // إذا تغير الاسم، التحقق من عدم التكرار
        if (!existingFieldType.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
        {
            var allFieldTypes = await _fieldTypeRepository.GetAllFieldTypesAsync(cancellationToken);
            if (allFieldTypes.Any(ft => ft.Id != existingFieldType.Id && 
                                      ft.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.LogWarning("يوجد نوع حقل آخر بنفس الاسم: {Name}", request.Name);
                return ResultDto<Unit>.Failure($"يوجد نوع حقل آخر بالاسم '{request.Name}' مسبقاً");
            }
        }

        // التحقق من أن التغييرات لا تؤثر على الحقول الموجودة
        var relatedFields = await _unitTypeFieldRepository.GetAllAsync(cancellationToken);
        var fieldsUsingThisType = relatedFields.Where(f => f.FieldTypeId == existingFieldType.Id).ToList();
        
        if (fieldsUsingThisType.Any() && !existingFieldType.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("محاولة تغيير نوع حقل مستخدم في حقول موجودة: {FieldTypeId}", existingFieldType.Id);
            return ResultDto<Unit>.Failure("لا يمكن تغيير نوع الحقل لأنه مستخدم في حقول موجودة");
        }

        // التحقق من صحة قواعد التحقق
        if (request.ValidationRules != null)
        {
            try
            {
                JsonConvert.SerializeObject(request.ValidationRules);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("قواعد التحقق غير صحيحة: {Error}", ex.Message);
                return ResultDto<Unit>.Failure("قواعد التحقق المدخلة غير صحيحة");
            }
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }
}

/// <summary>
/// حدث تحديث نوع الحقل
/// Field type updated event
/// </summary>
public class FieldTypeUpdatedEvent
{
    /// <summary>
    /// معرف نوع الحقل
    /// Field type ID
    /// </summary>
    public Guid FieldTypeId { get; set; }

    /// <summary>
    /// اسم نوع الحقل
    /// Field type name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// الاسم المعروض
    /// Display name
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// معرف المحدث
    /// Updated by user ID
    /// </summary>
    public Guid UpdatedBy { get; set; }

    /// <summary>
    /// تاريخ التحديث
    /// Update date
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

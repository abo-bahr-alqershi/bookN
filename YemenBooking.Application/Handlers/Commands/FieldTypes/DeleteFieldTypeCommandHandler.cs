using MediatR;
using Microsoft.Extensions.Logging;
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

namespace YemenBooking.Application.Handlers.Commands.FieldTypes;

/// <summary>
/// معالج أمر حذف نوع الحقل
/// Delete field type command handler
/// 
/// يقوم بحذف نوع حقل موجود ويشمل:
/// - التحقق من صحة البيانات المدخلة
/// - التحقق من وجود نوع الحقل
/// - التحقق من صلاحيات المستخدم (مسؤول فقط)
/// - التحقق من قواعد العمل
/// - حذف نوع الحقل (حذف ناعم)
/// - تسجيل العملية في سجل التدقيق
/// 
/// Deletes existing field type and includes:
/// - Input data validation
/// - Field type existence validation
/// - User authorization validation (Admin only)
/// - Business rules validation
/// - Field type deletion (soft delete)
/// - Audit log creation
/// </summary>
public class DeleteFieldTypeCommandHandler : IRequestHandler<DeleteFieldTypeCommand, ResultDto<Unit>>
{
    private readonly IFieldTypeRepository _fieldTypeRepository;
    private readonly IUnitTypeFieldRepository _unitTypeFieldRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidationService _validationService;
    private readonly IAuditService _auditService;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<DeleteFieldTypeCommandHandler> _logger;

    public DeleteFieldTypeCommandHandler(
        IFieldTypeRepository fieldTypeRepository,
        IUnitTypeFieldRepository unitTypeFieldRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IValidationService validationService,
        IAuditService auditService,
        IEventPublisher eventPublisher,
        ILogger<DeleteFieldTypeCommandHandler> logger)
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
    /// معالجة أمر حذف نوع الحقل
    /// Handle delete field type command
    /// </summary>
    /// <param name="request">طلب حذف نوع الحقل / Delete field type request</param>
    /// <param name="cancellationToken">رمز الإلغاء / Cancellation token</param>
    /// <returns>نتيجة العملية / Operation result</returns>
    public async Task<ResultDto<Unit>> Handle(DeleteFieldTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("بدء معالجة أمر حذف نوع الحقل: {FieldTypeId}", request.FieldTypeId);

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
            var businessRulesValidation = await ValidateBusinessRulesAsync(existingFieldType, cancellationToken);
            if (!businessRulesValidation.Success)
            {
                return businessRulesValidation;
            }

            // الخطوة 5: حفظ القيم الأصلية للمراجعة
            var originalValues = new
            {
                existingFieldType.Id,
                existingFieldType.Name,
                existingFieldType.DisplayName,
                existingFieldType.ValidationRules,
                existingFieldType.IsActive
            };

            // الخطوة 6: حذف نوع الحقل (حذف ناعم)
            existingFieldType.IsDeleted = true;
            existingFieldType.DeletedAt = DateTime.UtcNow;
            existingFieldType.DeletedBy = _currentUserService.UserId;

            await _fieldTypeRepository.UpdateFieldTypeAsync(existingFieldType, cancellationToken);

            // الخطوة 7: تسجيل العملية في سجل التدقيق
            await _auditService.LogActivityAsync(
                "FieldType",
                existingFieldType.Id.ToString(),
                "Delete",
                $"تم حذف نوع الحقل: {existingFieldType.Name}",
                originalValues,
                null,
                cancellationToken);

            // الخطوة 8: نشر الحدث
            await _eventPublisher.PublishEventAsync(new FieldTypeDeletedEvent
            {
                FieldTypeId = existingFieldType.Id,
                Name = existingFieldType.Name,
                DisplayName = existingFieldType.DisplayName,
                DeletedBy = _currentUserService.UserId,
                DeletedAt = existingFieldType.DeletedAt.Value
            }, cancellationToken);

            _logger.LogInformation("تم حذف نوع الحقل بنجاح: {FieldTypeId}", existingFieldType.Id);

            return ResultDto<Unit>.Ok(
                Unit.Value,
                "تم حذف نوع الحقل بنجاح"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في حذف نوع الحقل: {FieldTypeId}", request.FieldTypeId);
            return ResultDto<Unit>.Failure("حدث خطأ أثناء حذف نوع الحقل");
        }
    }

    /// <summary>
    /// التحقق من صحة البيانات المدخلة
    /// Validate input data
    /// </summary>
    private async Task<ResultDto<Unit>> ValidateInputAsync(DeleteFieldTypeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validationService.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors);
            _logger.LogWarning("بيانات غير صحيحة لحذف نوع الحقل: {Errors}", errors);
            return ResultDto<Unit>.Failure($"بيانات غير صحيحة: {errors}");
        }

        // التحقق من صحة معرف نوع الحقل
        if (request.FieldTypeId == Guid.Empty)
        {
            _logger.LogWarning("معرف نوع الحقل غير صحيح: {FieldTypeId}", request.FieldTypeId);
            return ResultDto<Unit>.Failure("معرف نوع الحقل غير صحيح");
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
            _logger.LogWarning("المستخدم {UserId} لا يملك صلاحية حذف أنواع الحقول", _currentUserService.UserId);
            return Task.FromResult(ResultDto<Unit>.Failure("غير مصرح لك بحذف أنواع الحقول"));
        }

        return Task.FromResult(ResultDto<Unit>.Ok(Unit.Value));
    }

    /// <summary>
    /// التحقق من قواعد العمل
    /// Validate business rules
    /// </summary>
    private async Task<ResultDto<Unit>> ValidateBusinessRulesAsync(FieldType fieldType, CancellationToken cancellationToken)
    {
        // التحقق من عدم حذف أنواع الحقول الأساسية
        var systemFieldTypes = new[] { "text", "number", "boolean", "date", "select", "multi_select", "file", "textarea", "email", "url" };
        if (systemFieldTypes.Contains(fieldType.Name.ToLower()))
        {
            _logger.LogWarning("محاولة حذف نوع حقل أساسي للنظام: {Name}", fieldType.Name);
            return ResultDto<Unit>.Failure("لا يمكن حذف أنواع الحقول الأساسية للنظام");
        }

        // التحقق من عدم وجود حقول مرتبطة بهذا النوع
        var allFields = await _unitTypeFieldRepository.GetAllAsync(cancellationToken);
        var relatedFields = allFields.Where(f => f.FieldTypeId == fieldType.Id && !f.IsDeleted).ToList();
        
        if (relatedFields.Any())
        {
            _logger.LogWarning("محاولة حذف نوع حقل مستخدم في حقول موجودة: {FieldTypeId} - عدد الحقول: {Count}", 
                fieldType.Id, relatedFields.Count);
            return ResultDto<Unit>.Failure($"لا يمكن حذف نوع الحقل لأنه مستخدم في {relatedFields.Count} حقل/حقول");
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }
}

/// <summary>
/// حدث حذف نوع الحقل
/// Field type deleted event
/// </summary>
public class FieldTypeDeletedEvent
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
    /// معرف المحذوف
    /// Deleted by user ID
    /// </summary>
    public Guid DeletedBy { get; set; }

    /// <summary>
    /// تاريخ الحذف
    /// Deletion date
    /// </summary>
    public DateTime DeletedAt { get; set; }
}

using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using YemenBooking.Application.Commands.FieldTypes;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Commands.FieldTypes;

/// <summary>
/// معالج أمر إنشاء نوع الحقل
/// Create field type command handler
/// 
/// يقوم بإنشاء نوع حقل جديد ويشمل:
/// - التحقق من صحة البيانات المدخلة
/// - التحقق من صلاحيات المستخدم (مسؤول فقط)
/// - التحقق من قواعد العمل
/// - إنشاء نوع الحقل
/// - تسجيل العملية في سجل التدقيق
/// 
/// Creates new field type and includes:
/// - Input data validation
/// - User authorization validation (Admin only)
/// - Business rules validation
/// - Field type creation
/// - Audit log creation
/// </summary>
public class CreateFieldTypeCommandHandler : IRequestHandler<CreateFieldTypeCommand, ResultDto<string>>
{
    private readonly IFieldTypeRepository _fieldTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidationService _validationService;
    private readonly IAuditService _auditService;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<CreateFieldTypeCommandHandler> _logger;

    public CreateFieldTypeCommandHandler(
        IFieldTypeRepository fieldTypeRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IValidationService validationService,
        IAuditService auditService,
        IEventPublisher eventPublisher,
        ILogger<CreateFieldTypeCommandHandler> logger)
    {
        _fieldTypeRepository = fieldTypeRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _validationService = validationService;
        _auditService = auditService;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    /// <summary>
    /// معالجة أمر إنشاء نوع الحقل
    /// Handle create field type command
    /// </summary>
    /// <param name="request">طلب إنشاء نوع الحقل / Create field type request</param>
    /// <param name="cancellationToken">رمز الإلغاء / Cancellation token</param>
    /// <returns>نتيجة العملية مع معرف نوع الحقل / Operation result with field type ID</returns>
    public async Task<ResultDto<string>> Handle(CreateFieldTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("بدء معالجة أمر إنشاء نوع حقل جديد: {Name}", request.Name);

            // الخطوة 1: التحقق من صحة البيانات المدخلة
            var inputValidationResult = await ValidateInputAsync(request, cancellationToken);
            if (!inputValidationResult.Success)
            {
                return inputValidationResult;
            }

            // الخطوة 2: التحقق من صلاحيات المستخدم
            var authorizationValidation = await ValidateAuthorizationAsync(cancellationToken);
            if (!authorizationValidation.Success)
            {
                return authorizationValidation;
            }

            // الخطوة 3: التحقق من قواعد العمل
            var businessRulesValidation = await ValidateBusinessRulesAsync(request, cancellationToken);
            if (!businessRulesValidation.Success)
            {
                return businessRulesValidation;
            }

            // الخطوة 4: إنشاء نوع الحقل
            var fieldType = new FieldType
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                DisplayName = request.DisplayName,
                ValidationRules = JsonConvert.SerializeObject(request.ValidationRules ?? new System.Collections.Generic.Dictionary<string, object>()),
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUserService.UserId,
                IsDeleted = false
            };

            var createdFieldType = await _fieldTypeRepository.CreateFieldTypeAsync(fieldType, cancellationToken);

            // الخطوة 5: تسجيل العملية في سجل التدقيق
            await _auditService.LogActivityAsync(
                "FieldType",
                createdFieldType.Id.ToString(),
                "Create",
                $"تم إنشاء نوع حقل جديد: {createdFieldType.Name}",
                null,
                createdFieldType,
                cancellationToken);

            // الخطوة 6: نشر الحدث
            // await _eventPublisher.PublishEventAsync(new FieldTypeCreatedEvent
            // {
            //     FieldTypeId = createdFieldType.Id,
            //     Name = createdFieldType.Name,
            //     DisplayName = createdFieldType.DisplayName,
            //     CreatedBy = _currentUserService.UserId,
            //     CreatedAt = createdFieldType.CreatedAt
            // }, cancellationToken);

            _logger.LogInformation("تم إنشاء نوع الحقل بنجاح: {FieldTypeId}", createdFieldType.Id);

            return ResultDto<string>.Ok(
                createdFieldType.Id.ToString(),
                "تم إنشاء نوع الحقل بنجاح"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في إنشاء نوع الحقل: {Name}", request.Name);
            return ResultDto<string>.Failure("حدث خطأ أثناء إنشاء نوع الحقل");
        }
    }

    /// <summary>
    /// التحقق من صحة البيانات المدخلة
    /// Validate input data
    /// </summary>
    private async Task<ResultDto<string>> ValidateInputAsync(CreateFieldTypeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validationService.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors);
            _logger.LogWarning("بيانات غير صحيحة لإنشاء نوع الحقل: {Errors}", errors);
            return ResultDto<string>.Failure($"بيانات غير صحيحة: {errors}");
        }

        return ResultDto<string>.Ok(string.Empty);
    }

    /// <summary>
    /// التحقق من صلاحيات المستخدم
    /// Validate user authorization
    /// </summary>
    private Task<ResultDto<string>> ValidateAuthorizationAsync(CancellationToken cancellationToken)
    {
        if (_currentUserService.Role != "Admin")
        {
            _logger.LogWarning("المستخدم {UserId} لا يملك صلاحية إنشاء أنواع الحقول", _currentUserService.UserId);
            return Task.FromResult(ResultDto<string>.Failure("غير مصرح لك بإنشاء أنواع الحقول"));
        }

        return Task.FromResult(ResultDto<string>.Ok(string.Empty));
    }

    /// <summary>
    /// التحقق من قواعد العمل
    /// Validate business rules
    /// </summary>
    private async Task<ResultDto<string>> ValidateBusinessRulesAsync(CreateFieldTypeCommand request, CancellationToken cancellationToken)
    {
        // التحقق من عدم وجود نوع حقل بنفس الاسم
        var existingFieldTypes = await _fieldTypeRepository.GetAllFieldTypesAsync(cancellationToken);
        if (existingFieldTypes.Any(ft => ft.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
        {
            _logger.LogWarning("يوجد نوع حقل بنفس الاسم: {Name}", request.Name);
            return ResultDto<string>.Failure($"يوجد نوع حقل بالاسم '{request.Name}' مسبقاً");
        }

        // التحقق من أن نوع الحقل مدعوم
        var supportedFieldTypes = new[] { "text", "number", "boolean", "date", "select", "multi_select", "file", "textarea", "email", "url", "color", "range" };
        if (!supportedFieldTypes.Contains(request.Name.ToLower()))
        {
            _logger.LogWarning("نوع الحقل غير مدعوم: {Name}", request.Name);
            return ResultDto<string>.Failure($"نوع الحقل '{request.Name}' غير مدعوم");
        }

        // التحقق من صحة قواعد التحقق
        if (request.ValidationRules != null)
        {
            try
            {
                JsonConvert.SerializeObject(request.ValidationRules);
            }
            catch (JsonException ex)
            {
                _logger.LogWarning("قواعد التحقق غير صحيحة: {Error}", ex.Message);
                return ResultDto<string>.Failure("قواعد التحقق المدخلة غير صحيحة");
            }
        }

        return ResultDto<string>.Ok(string.Empty);
    }
}

/// <summary>
/// حدث إنشاء نوع الحقل
/// Field type created event
/// </summary>
public class FieldTypeCreatedEvent
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
    /// معرف المنشئ
    /// Created by user ID
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// تاريخ الإنشاء
    /// Creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

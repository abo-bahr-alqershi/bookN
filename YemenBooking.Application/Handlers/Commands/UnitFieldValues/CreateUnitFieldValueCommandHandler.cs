using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YemenBooking.Application.Commands.UnitFieldValues;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Commands.UnitFieldValues;

/// <summary>
/// معالج أمر إنشاء قيمة حقل الوحدة
/// Create unit field value command handler
/// 
/// يقوم بإنشاء قيمة جديدة للحقل الديناميكي للوحدة ويشمل:
/// - التحقق من صحة البيانات المدخلة
/// - التحقق من وجود الوحدة والحقل
/// - التحقق من صلاحيات المستخدم (مالك العقار أو مسؤول)
/// - التحقق من قواعد العمل
/// - التحقق من صحة القيمة حسب نوع الحقل
/// - إنشاء قيمة الحقل
/// - تسجيل العملية في سجل التدقيق
/// - نشر الأحداث
/// 
/// Creates new unit field value and includes:
/// - Input data validation
/// - Unit and field existence validation
/// - User authorization validation (Property owner or Admin)
/// - Business rules validation
/// - Field value validation according to field type
/// - Unit field value creation
/// - Audit log creation
/// - Event publishing
/// </summary>
public class CreateUnitFieldValueCommandHandler : IRequestHandler<CreateUnitFieldValueCommand, ResultDto<Guid>>
{
    private readonly IUnitFieldValueRepository _unitFieldValueRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitTypeFieldRepository _unitTypeFieldRepository;
    private readonly IFieldTypeRepository _fieldTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidationService _validationService;
    private readonly IAuditService _auditService;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<CreateUnitFieldValueCommandHandler> _logger;

    public CreateUnitFieldValueCommandHandler(
        IUnitFieldValueRepository unitFieldValueRepository,
        IUnitRepository unitRepository,
        IPropertyRepository propertyRepository,
        IUnitTypeFieldRepository unitTypeFieldRepository,
        IFieldTypeRepository fieldTypeRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IValidationService validationService,
        IAuditService auditService,
        IEventPublisher eventPublisher,
        ILogger<CreateUnitFieldValueCommandHandler> logger)
    {
        _unitFieldValueRepository = unitFieldValueRepository;
        _unitRepository = unitRepository;
        _propertyRepository = propertyRepository;
        _unitTypeFieldRepository = unitTypeFieldRepository;
        _fieldTypeRepository = fieldTypeRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _validationService = validationService;
        _auditService = auditService;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    /// <summary>
    /// معالجة أمر إنشاء قيمة حقل الوحدة
    /// Handle create unit field value command
    /// </summary>
    /// <param name="request">طلب إنشاء قيمة الحقل / Create field value request</param>
    /// <param name="cancellationToken">رمز الإلغاء / Cancellation token</param>
    /// <returns>نتيجة العملية مع معرف القيمة / Operation result with value ID</returns>
    public async Task<ResultDto<Guid>> Handle(CreateUnitFieldValueCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("بدء معالجة أمر إنشاء قيمة حقل الوحدة: UnitId={UnitId}, FieldId={FieldId}", 
                request.UnitId, request.FieldId);

            // الخطوة 1: التحقق من صحة البيانات المدخلة
            var inputValidationResult = ValidateInput(request);
            if (!inputValidationResult.Success)
            {
                return inputValidationResult;
            }

            // الخطوة 2: التحقق من وجود الوحدة
            var unit = await _unitRepository.GetUnitByIdAsync(request.UnitId, cancellationToken);
            if (unit == null || unit.IsDeleted)
            {
                _logger.LogWarning("الوحدة غير موجودة: {UnitId}", request.UnitId);
                return ResultDto<Guid>.Failure("الوحدة غير موجودة");
            }

            // الخطوة 3: التحقق من وجود العقار
            var property = await _propertyRepository.GetPropertyByIdAsync(unit.PropertyId, cancellationToken);
            if (property == null || property.IsDeleted)
            {
                _logger.LogWarning("العقار غير موجود: {PropertyId}", unit.PropertyId);
                return ResultDto<Guid>.Failure("العقار غير موجود");
            }

            // الخطوة 4: التحقق من وجود الحقل
            var field = await _unitTypeFieldRepository.GetUnitTypeFieldByIdAsync(request.FieldId, cancellationToken);
            if (field == null || field.IsDeleted)
            {
                _logger.LogWarning("الحقل غير موجود: {FieldId}", request.FieldId);
                return ResultDto<Guid>.Failure("الحقل غير موجود");
            }

            // الخطوة 5: التحقق من صلاحيات المستخدم
            var authorizationValidation = ValidateAuthorization(property);
            if (!authorizationValidation.Success)
            {
                return authorizationValidation;
            }

            // الخطوة 6: التحقق من قواعد العمل
            var businessRulesValidation = await ValidateBusinessRulesAsync(request, unit, property, field, cancellationToken);
            if (!businessRulesValidation.Success)
            {
                return businessRulesValidation;
            }

            // الخطوة 7: التحقق من صحة القيمة
            var fieldValueValidation = await ValidateFieldValueAsync(request, field, cancellationToken);
            if (!fieldValueValidation.Success)
            {
                return fieldValueValidation;
            }

            // الخطوة 8: إنشاء قيمة الحقل
            var unitFieldValue = new UnitFieldValue
            {
                Id = Guid.NewGuid(),
                UnitId = request.UnitId,
                UnitTypeFieldId = request.FieldId,
                FieldValue = request.FieldValue?.Trim() ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var createdValue = await _unitFieldValueRepository.CreateUnitFieldValueAsync(unitFieldValue, cancellationToken);

            // الخطوة 9: تسجيل العملية في سجل التدقيق
            await _auditService.LogAsync(
                "UnitFieldValue",
                createdValue.Id.ToString(),
                $"تم إنشاء قيمة حقل جديدة: {field.FieldName} للوحدة: {unit.Name}",
                _currentUserService.UserId);

            // الخطوة 10: نشر الحدث
            // await _eventPublisher.PublishEventAsync(new UnitFieldValueCreatedEvent
            // {
            //     ValueId = createdValue.Id,
            //     UnitId = createdValue.UnitId,
            //     PropertyId = unit.PropertyId,
            //     FieldId = createdValue.UnitTypeFieldId,
            //     FieldName = field.FieldName,
            //     FieldValue = createdValue.FieldValue,
            //     CreatedBy = _currentUserService.UserId,
            //     CreatedAt = createdValue.CreatedAt
            // }, cancellationToken);

            _logger.LogInformation("تم إنشاء قيمة حقل الوحدة بنجاح: {ValueId}", createdValue.Id);

            return ResultDto<Guid>.Ok(
                createdValue.Id,
                "تم إنشاء قيمة الحقل بنجاح"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في إنشاء قيمة حقل الوحدة: UnitId={UnitId}, FieldId={FieldId}", 
                request.UnitId, request.FieldId);
            return ResultDto<Guid>.Failure("حدث خطأ أثناء إنشاء قيمة الحقل");
        }
    }

    /// <summary>
    /// التحقق من صحة البيانات المدخلة
    /// Validate input data
    /// </summary>
    private ResultDto<Guid> ValidateInput(CreateUnitFieldValueCommand request)
    {
        var validationErrors = new List<string>();

        // التحقق من معرف الوحدة
        if (request.UnitId == Guid.Empty)
        {
            validationErrors.Add("معرف الوحدة مطلوب");
        }

        // التحقق من معرف الحقل
        if (request.FieldId == Guid.Empty)
        {
            validationErrors.Add("معرف الحقل مطلوب");
        }

        // التحقق من قيمة الحقل (يمكن أن تكون فارغة للحقول الاختيارية)
        if (request.FieldValue != null && request.FieldValue.Length > 2000)
        {
            validationErrors.Add("قيمة الحقل يجب أن تكون أقل من 2000 حرف");
        }

        if (validationErrors.Any())
        {
            var errorMessage = string.Join(", ", validationErrors);
            _logger.LogWarning("فشل التحقق من البيانات المدخلة: {Errors}", errorMessage);
            return ResultDto<Guid>.Failure($"خطأ في البيانات المدخلة: {errorMessage}");
        }

        return ResultDto<Guid>.Ok(Guid.Empty);
    }

    /// <summary>
    /// التحقق من صلاحيات المستخدم
    /// Validate user authorization
    /// </summary>
    private ResultDto<Guid> ValidateAuthorization(Property property)
    {
        // المسؤولون يمكنهم إدارة قيم جميع الوحدات
        if (_currentUserService.Role == "Admin")
        {
            return ResultDto<Guid>.Ok(Guid.Empty);
        }

        // مالك العقار يمكنه إدارة قيم وحدات عقاره
        if (property.OwnerId == _currentUserService.UserId)
        {
            return ResultDto<Guid>.Ok(Guid.Empty);
        }

        // موظفو العقار يمكنهم إدارة قيم الوحدات
        if (_currentUserService.IsStaffInProperty(property.Id))
        {
            return ResultDto<Guid>.Ok(Guid.Empty);
        }

        _logger.LogWarning("محاولة إنشاء قيمة حقل من مستخدم غير مصرح له: UserId={UserId}, PropertyId={PropertyId}", 
            _currentUserService.UserId, property.Id);
        return ResultDto<Guid>.Failure("ليس لديك صلاحية لإضافة قيم حقول هذه الوحدة");
    }

    /// <summary>
    /// التحقق من قواعد العمل
    /// Validate business rules
    /// </summary>
    private async Task<ResultDto<Guid>> ValidateBusinessRulesAsync(CreateUnitFieldValueCommand request, Core.Entities.Unit unit, Property property, UnitTypeField field, CancellationToken cancellationToken)
    {
        // التحقق من أن الحقل ينتمي لنوع العقار الصحيح
        if (field.UnitTypeId != property.TypeId)
        {
            _logger.LogWarning("الحقل لا ينتمي لنوع العقار: FieldId={FieldId}, PropertyTypeId={PropertyTypeId}", 
                request.FieldId, property.TypeId);
            return ResultDto<Guid>.Failure("هذا الحقل غير متوفر لنوع العقار المحدد");
        }

        // التحقق من عدم وجود قيمة مسبقة للحقل (إذا كان الحقل لا يدعم قيم متعددة)
        var existingValues = await _unitFieldValueRepository.GetValuesByUnitIdAsync(request.UnitId, cancellationToken);
        var existingValue = existingValues.FirstOrDefault(v => v.UnitTypeFieldId == request.FieldId);
        if (existingValue != null)
        {
            _logger.LogWarning("يوجد قيمة مسبقة للحقل: UnitId={UnitId}, FieldId={FieldId}", 
                request.UnitId, request.FieldId);
            return ResultDto<Guid>.Failure("يوجد قيمة مسبقة لهذا الحقل في الوحدة");
        }

        return ResultDto<Guid>.Ok(Guid.Empty);
    }

    /// <summary>
    /// التحقق من صحة قيمة الحقل
    /// Validate field value
    /// </summary>
    private async Task<ResultDto<Guid>> ValidateFieldValueAsync(CreateUnitFieldValueCommand request, UnitTypeField field, CancellationToken cancellationToken)
    {
        // التحقق من الحقول المطلوبة
        if (field.IsRequired && string.IsNullOrWhiteSpace(request.FieldValue))
        {
            _logger.LogWarning("قيمة مطلوبة للحقل الإلزامي: {FieldName}", field.FieldName);
            return ResultDto<Guid>.Failure($"قيمة الحقل '{field.DisplayName}' مطلوبة");
        }

        // إذا كانت القيمة فارغة ولكن الحقل اختياري، فالأمر صحيح
        if (string.IsNullOrWhiteSpace(request.FieldValue))
        {
            return ResultDto<Guid>.Ok(Guid.Empty);
        }

        // التحقق من نوع الحقل وقواعد التحقق
        var fieldType = await _fieldTypeRepository.GetFieldTypeByIdAsync(field.FieldTypeId, cancellationToken);
        if (fieldType == null)
        {
            _logger.LogWarning("نوع الحقل غير موجود: {FieldTypeId}", field.FieldTypeId);
            return ResultDto<Guid>.Failure("نوع الحقل غير صحيح");
        }

        // التحقق حسب نوع الحقل
        var fieldTypeValidation = ValidateByFieldType(request.FieldValue, fieldType, field);
        if (!fieldTypeValidation.Success)
        {
            return fieldTypeValidation;
        }

        // التحقق من قواعد التحقق المخصصة
        if (!string.IsNullOrWhiteSpace(field.ValidationRules))
        {
            var customValidation = ValidateCustomRules(request.FieldValue, field.ValidationRules);
            if (!customValidation.Success)
            {
                return customValidation;
            }
        }

        return ResultDto<Guid>.Ok(Guid.Empty);
    }

    /// <summary>
    /// التحقق حسب نوع الحقل
    /// Validate by field type
    /// </summary>
    private ResultDto<Guid> ValidateByFieldType(string fieldValue, FieldType fieldType, UnitTypeField field)
    {
        switch (fieldType.Name.ToLower())
        {
            case "number":
            case "integer":
                if (!decimal.TryParse(fieldValue, out _))
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون رقم");
                }
                break;

            case "boolean":
                if (!bool.TryParse(fieldValue, out _) && 
                    !fieldValue.ToLower().Equals("true") && 
                    !fieldValue.ToLower().Equals("false") &&
                    !fieldValue.Equals("1") && 
                    !fieldValue.Equals("0"))
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون true أو false");
                }
                break;

            case "date":
            case "datetime":
                if (!DateTime.TryParse(fieldValue, out _))
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون تاريخ صحيح");
                }
                break;

            case "email":
                if (!IsValidEmail(fieldValue))
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون بريد إلكتروني صحيح");
                }
                break;

            case "url":
                if (!Uri.TryCreate(fieldValue, UriKind.Absolute, out _))
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون رابط صحيح");
                }
                break;

            case "select":
            case "multi_select":
                var optionsValidation = ValidateSelectOptions(fieldValue, field, fieldType.Name.ToLower() == "multi_select");
                if (!optionsValidation.Success)
                {
                    return optionsValidation;
                }
                break;

            case "phone":
                if (!IsValidPhoneNumber(fieldValue))
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون رقم هاتف صحيح");
                }
                break;
        }

        return ResultDto<Guid>.Ok(Guid.Empty);
    }

    /// <summary>
    /// التحقق من خيارات حقول الاختيار
    /// Validate select field options
    /// </summary>
    private ResultDto<Guid> ValidateSelectOptions(string fieldValue, UnitTypeField field, bool isMultiSelect)
    {
        if (string.IsNullOrWhiteSpace(field.FieldOptions))
        {
            return ResultDto<Guid>.Failure($"خيارات الحقل '{field.DisplayName}' غير محددة");
        }

        try
        {
            var options = JsonSerializer.Deserialize<Dictionary<string, object>>(field.FieldOptions);
            var availableOptions = options?.Keys.ToList() ?? new List<string>();

            if (isMultiSelect)
            {
                // للحقول متعددة الاختيار، قد تكون القيم مفصولة بفاصلة
                var selectedValues = fieldValue.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                               .Select(v => v.Trim())
                                               .ToList();

                foreach (var value in selectedValues)
                {
                    if (!availableOptions.Contains(value))
                    {
                        return ResultDto<Guid>.Failure($"القيمة '{value}' غير متوفرة في خيارات الحقل '{field.DisplayName}'");
                    }
                }
            }
            else
            {
                // للحقول أحادية الاختيار
                if (!availableOptions.Contains(fieldValue))
                {
                    return ResultDto<Guid>.Failure($"القيمة '{fieldValue}' غير متوفرة في خيارات الحقل '{field.DisplayName}'");
                }
            }
        }
        catch (JsonException)
        {
            _logger.LogWarning("خطأ في تحليل خيارات الحقل: {FieldId}", field.Id);
            return ResultDto<Guid>.Failure($"خيارات الحقل '{field.DisplayName}' غير صحيحة");
        }

        return ResultDto<Guid>.Ok(Guid.Empty);
    }

    /// <summary>
    /// التحقق من قواعد التحقق المخصصة
    /// Validate custom validation rules
    /// </summary>
    private ResultDto<Guid> ValidateCustomRules(string fieldValue, string validationRules)
    {
        try
        {
            var rules = JsonSerializer.Deserialize<Dictionary<string, object>>(validationRules);
            
            if (rules == null)
            {
                return ResultDto<Guid>.Ok(Guid.Empty);
            }

            // التحقق من الحد الأدنى والأقصى للطول
            if (rules.ContainsKey("minLength") && int.TryParse(rules["minLength"]?.ToString(), out int minLength))
            {
                if (fieldValue.Length < minLength)
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل يجب أن تكون على الأقل {minLength} أحرف");
                }
            }

            if (rules.ContainsKey("maxLength") && int.TryParse(rules["maxLength"]?.ToString(), out int maxLength))
            {
                if (fieldValue.Length > maxLength)
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل يجب أن تكون أقل من {maxLength} أحرف");
                }
            }

            // التحقق من الحد الأدنى والأقصى للقيم الرقمية
            if (rules.ContainsKey("min") && decimal.TryParse(fieldValue, out decimal numericValue))
            {
                if (decimal.TryParse(rules["min"]?.ToString(), out decimal minValue) && numericValue < minValue)
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل يجب أن تكون على الأقل {minValue}");
                }
            }

            if (rules.ContainsKey("max") && decimal.TryParse(fieldValue, out decimal numericValue2))
            {
                if (decimal.TryParse(rules["max"]?.ToString(), out decimal maxValue) && numericValue2 > maxValue)
                {
                    return ResultDto<Guid>.Failure($"قيمة الحقل يجب أن تكون أقل من {maxValue}");
                }
            }

            // التحقق من النمط (Pattern/Regex)
            if (rules.ContainsKey("pattern"))
            {
                var pattern = rules["pattern"]?.ToString();
                if (!string.IsNullOrEmpty(pattern) && !System.Text.RegularExpressions.Regex.IsMatch(fieldValue, pattern))
                {
                    return ResultDto<Guid>.Failure("قيمة الحقل لا تطابق النمط المطلوب");
                }
            }
        }
        catch (JsonException)
        {
            _logger.LogWarning("خطأ في تحليل قواعد التحقق المخصصة");
        }

        return ResultDto<Guid>.Ok(Guid.Empty);
    }

    /// <summary>
    /// التحقق من صحة البريد الإلكتروني
    /// Validate email format
    /// </summary>
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// التحقق من صحة رقم الهاتف
    /// Validate phone number format
    /// </summary>
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // تحقق أساسي من رقم الهاتف
        var cleaned = System.Text.RegularExpressions.Regex.Replace(phoneNumber, @"[^\d+]", "");
        return cleaned.Length >= 8 && cleaned.Length <= 15;
    }
}

/// <summary>
/// حدث إنشاء قيمة حقل الوحدة
/// Unit field value created event
/// </summary>
public class UnitFieldValueCreatedEvent
{
    /// <summary>
    /// معرف القيمة
    /// Value ID
    /// </summary>
    public Guid ValueId { get; set; }

    /// <summary>
    /// معرف الوحدة
    /// Unit ID
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// معرف الحقل
    /// Field ID
    /// </summary>
    public Guid FieldId { get; set; }

    /// <summary>
    /// اسم الحقل
    /// Field name
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// قيمة الحقل
    /// Field value
    /// </summary>
    public string FieldValue { get; set; } = string.Empty;

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

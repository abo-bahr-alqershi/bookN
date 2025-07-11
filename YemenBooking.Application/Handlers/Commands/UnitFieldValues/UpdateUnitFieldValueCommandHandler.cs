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
using Unit = MediatR.Unit;

namespace YemenBooking.Application.Handlers.Commands.UnitFieldValues;

/// <summary>
/// معالج أمر تحديث قيمة حقل الوحدة
/// Update unit field value command handler
/// 
/// يقوم بتحديث قيمة الحقل الديناميكي للوحدة ويشمل:
/// - التحقق من صحة البيانات المدخلة
/// - التحقق من وجود القيمة والوحدة والحقل
/// - التحقق من صلاحيات المستخدم (مالك العقار أو مسؤول)
/// - التحقق من قواعد العمل
/// - التحقق من صحة القيمة الجديدة حسب نوع الحقل
/// - تحديث قيمة الحقل
/// - تسجيل العملية في سجل التدقيق
/// - نشر الأحداث
/// 
/// Updates unit field value and includes:
/// - Input data validation
/// - Value, unit and field existence validation
/// - User authorization validation (Property owner or Admin)
/// - Business rules validation
/// - New field value validation according to field type
/// - Unit field value update
/// - Audit log creation
/// - Event publishing
/// </summary>
public class UpdateUnitFieldValueCommandHandler : IRequestHandler<UpdateUnitFieldValueCommand, ResultDto<Unit>>
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
    private readonly ILogger<UpdateUnitFieldValueCommandHandler> _logger;

    public UpdateUnitFieldValueCommandHandler(
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
        ILogger<UpdateUnitFieldValueCommandHandler> logger)
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
    /// معالجة أمر تحديث قيمة حقل الوحدة
    /// Handle update unit field value command
    /// </summary>
    /// <param name="request">طلب تحديث قيمة الحقل / Update field value request</param>
    /// <param name="cancellationToken">رمز الإلغاء / Cancellation token</param>
    /// <returns>نتيجة العملية / Operation result</returns>
    public async Task<ResultDto<Unit>> Handle(UpdateUnitFieldValueCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("بدء معالجة أمر تحديث قيمة حقل الوحدة: ValueId={ValueId}", 
                request.ValueId);

            // الخطوة 1: التحقق من صحة البيانات المدخلة
            var inputValidationResult = ValidateInput(request);
            if (!inputValidationResult.Success)
            {
                return inputValidationResult;
            }

            // الخطوة 2: التحقق من وجود قيمة الحقل
            var fieldValue = await _unitFieldValueRepository.GetUnitFieldValueByIdAsync(request.ValueId, cancellationToken);
            if (fieldValue == null || fieldValue.IsDeleted)
            {
                _logger.LogWarning("قيمة الحقل غير موجودة: {ValueId}", request.ValueId);
                return ResultDto<Unit>.Failure("قيمة الحقل غير موجودة");
            }

            // الخطوة 3: التحقق من وجود الوحدة
            var unit = await _unitRepository.GetUnitByIdAsync(fieldValue.UnitId, cancellationToken);
            if (unit == null || unit.IsDeleted)
            {
                _logger.LogWarning("الوحدة غير موجودة: {UnitId}", fieldValue.UnitId);
                return ResultDto<Unit>.Failure("الوحدة غير موجودة");
            }

            // الخطوة 4: التحقق من وجود العقار
            var property = await _propertyRepository.GetPropertyByIdAsync(unit.PropertyId, cancellationToken);
            if (property == null || property.IsDeleted)
            {
                _logger.LogWarning("العقار غير موجود: {PropertyId}", unit.PropertyId);
                return ResultDto<Unit>.Failure("العقار غير موجود");
            }

            // الخطوة 5: التحقق من وجود الحقل
            var field = await _unitTypeFieldRepository.GetUnitTypeFieldByIdAsync(fieldValue.UnitTypeFieldId, cancellationToken);
            if (field == null || field.IsDeleted)
            {
                _logger.LogWarning("الحقل غير موجود: {FieldId}", fieldValue.UnitTypeFieldId);
                return ResultDto<Unit>.Failure("الحقل غير موجود");
            }

            // الخطوة 6: التحقق من صلاحيات المستخدم
            var authorizationValidation = ValidateAuthorization(property);
            if (!authorizationValidation.Success)
            {
                return authorizationValidation;
            }

            // الخطوة 7: التحقق من قواعد العمل
            var businessRulesValidation = ValidateBusinessRules(request, unit, property, field);
            if (!businessRulesValidation.Success)
            {
                return businessRulesValidation;
            }

            // الخطوة 8: التحقق من صحة القيمة الجديدة
            var newFieldValueValidation = await ValidateNewFieldValueAsync(request, field, cancellationToken);
            if (!newFieldValueValidation.Success)
            {
                return newFieldValueValidation;
            }

            // الخطوة 9: حفظ القيمة القديمة للتدقيق
            var oldValue = new
            {
                fieldValue.Id,
                fieldValue.UnitId,
                fieldValue.UnitTypeFieldId,
                fieldValue.FieldValue,
                fieldValue.UpdatedAt
            };

            // الخطوة 10: تحديث قيمة الحقل
            fieldValue.FieldValue = request.NewFieldValue?.Trim() ?? string.Empty;
            fieldValue.UpdatedAt = DateTime.UtcNow;

            var updatedValue = await _unitFieldValueRepository.UpdateUnitFieldValueAsync(fieldValue, cancellationToken);

            // الخطوة 11: تسجيل العملية في سجل التدقيق
            await _auditService.LogActivityAsync(
                "UnitFieldValue",
                updatedValue.Id.ToString(),
                "Update",
                $"تم تحديث قيمة حقل: {field.FieldName} للوحدة: {unit.Name}",
                oldValue,
                updatedValue,
                cancellationToken);

            // الخطوة 12: نشر الحدث
            // await _eventPublisher.PublishEventAsync(new UnitFieldValueUpdatedEvent
            // {
            //     ValueId = updatedValue.Id,
            //     UnitId = updatedValue.UnitId,
            //     PropertyId = unit.PropertyId,
            //     FieldId = updatedValue.UnitTypeFieldId,
            //     FieldName = field.FieldName,
            //     OldFieldValue = oldValue.FieldValue,
            //     NewFieldValue = updatedValue.FieldValue,
            //     UpdatedBy = _currentUserService.UserId,
            //     UpdatedAt = updatedValue.UpdatedAt
            // }, cancellationToken);

            _logger.LogInformation("تم تحديث قيمة حقل الوحدة بنجاح: {ValueId}", updatedValue.Id);

            return ResultDto<Unit>.Ok(
                Unit.Value,
                "تم تحديث قيمة الحقل بنجاح"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في تحديث قيمة حقل الوحدة: ValueId={ValueId}", 
                request.ValueId);
            return ResultDto<Unit>.Failure("حدث خطأ أثناء تحديث قيمة الحقل");
        }
    }

    /// <summary>
    /// التحقق من صحة البيانات المدخلة
    /// Validate input data
    /// </summary>
    private ResultDto<Unit> ValidateInput(UpdateUnitFieldValueCommand request)
    {
        var validationErrors = new List<string>();

        // التحقق من معرف القيمة
        if (request.ValueId == Guid.Empty)
        {
            validationErrors.Add("معرف قيمة الحقل مطلوب");
        }

        // التحقق من القيمة الجديدة (يمكن أن تكون فارغة للحقول الاختيارية)
        if (request.NewFieldValue != null && request.NewFieldValue.Length > 2000)
        {
            validationErrors.Add("قيمة الحقل يجب أن تكون أقل من 2000 حرف");
        }

        if (validationErrors.Any())
        {
            var errorMessage = string.Join(", ", validationErrors);
            _logger.LogWarning("فشل التحقق من البيانات المدخلة: {Errors}", errorMessage);
            return ResultDto<Unit>.Failure($"خطأ في البيانات المدخلة: {errorMessage}");
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }

    /// <summary>
    /// التحقق من صلاحيات المستخدم
    /// Validate user authorization
    /// </summary>
    private ResultDto<Unit> ValidateAuthorization(Property property)
    {
        // المسؤولون يمكنهم إدارة قيم جميع الوحدات
        if (_currentUserService.Role == "Admin")
        {
            return ResultDto<Unit>.Ok(Unit.Value);
        }

        // مالك العقار يمكنه إدارة قيم وحدات عقاره
        if (property.OwnerId == _currentUserService.UserId)
        {
            return ResultDto<Unit>.Ok(Unit.Value);
        }

        // موظفو العقار يمكنهم إدارة قيم الوحدات
        if (_currentUserService.IsStaffInProperty(property.Id))
        {
            return ResultDto<Unit>.Ok(Unit.Value);
        }

        _logger.LogWarning("محاولة تحديث قيمة حقل من مستخدم غير مصرح له: UserId={UserId}, PropertyId={PropertyId}", 
            _currentUserService.UserId, property.Id);
        return ResultDto<Unit>.Failure("ليس لديك صلاحية لتحديث قيم حقول هذه الوحدة");
    }

    /// <summary>
    /// التحقق من قواعد العمل
    /// Validate business rules
    /// </summary>
    private ResultDto<Unit> ValidateBusinessRules(UpdateUnitFieldValueCommand request, Core.Entities.Unit unit, Property property, UnitTypeField field)
    {
        // التحقق من أن الحقل ينتمي لنوع العقار الصحيح
        if (field.UnitTypeId != property.TypeId)
        {
            _logger.LogWarning("الحقل لا ينتمي لنوع العقار: FieldId={FieldId}, PropertyTypeId={PropertyTypeId}", 
                field.Id, property.TypeId);
            return ResultDto<Unit>.Failure("هذا الحقل غير متوفر لنوع العقار المحدد");
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }

    /// <summary>
    /// التحقق من صحة القيمة الجديدة للحقل
    /// Validate new field value
    /// </summary>
    private async Task<ResultDto<Unit>> ValidateNewFieldValueAsync(UpdateUnitFieldValueCommand request, UnitTypeField field, CancellationToken cancellationToken)
    {
        // التحقق من الحقول المطلوبة
        if (field.IsRequired && string.IsNullOrWhiteSpace(request.NewFieldValue))
        {
            _logger.LogWarning("قيمة مطلوبة للحقل الإلزامي: {FieldName}", field.FieldName);
            return ResultDto<Unit>.Failure($"قيمة الحقل '{field.DisplayName}' مطلوبة");
        }

        // إذا كانت القيمة فارغة ولكن الحقل اختياري، فالأمر صحيح
        if (string.IsNullOrWhiteSpace(request.NewFieldValue))
        {
            return ResultDto<Unit>.Ok(Unit.Value);
        }

        // التحقق من نوع الحقل وقواعد التحقق
        var fieldType = await _fieldTypeRepository.GetFieldTypeByIdAsync(field.FieldTypeId, cancellationToken);
        if (fieldType == null)
        {
            _logger.LogWarning("نوع الحقل غير موجود: {FieldTypeId}", field.FieldTypeId);
            return ResultDto<Unit>.Failure("نوع الحقل غير صحيح");
        }

        // التحقق حسب نوع الحقل
        var fieldTypeValidation = ValidateByFieldType(request.NewFieldValue, fieldType, field);
        if (!fieldTypeValidation.Success)
        {
            return fieldTypeValidation;
        }

        // التحقق من قواعد التحقق المخصصة
        if (!string.IsNullOrWhiteSpace(field.ValidationRules))
        {
            var customValidation = ValidateCustomRules(request.NewFieldValue, field.ValidationRules);
            if (!customValidation.Success)
            {
                return customValidation;
            }
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }

    /// <summary>
    /// التحقق حسب نوع الحقل
    /// Validate by field type
    /// </summary>
    private ResultDto<Unit> ValidateByFieldType(string fieldValue, FieldType fieldType, UnitTypeField field)
    {
        switch (fieldType.Name.ToLower())
        {
            case "number":
            case "integer":
                if (!decimal.TryParse(fieldValue, out _))
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون رقم");
                }
                break;

            case "boolean":
                if (!bool.TryParse(fieldValue, out _) && 
                    !fieldValue.ToLower().Equals("true") && 
                    !fieldValue.ToLower().Equals("false") &&
                    !fieldValue.Equals("1") && 
                    !fieldValue.Equals("0"))
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون true أو false");
                }
                break;

            case "date":
            case "datetime":
                if (!DateTime.TryParse(fieldValue, out _))
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون تاريخ صحيح");
                }
                break;

            case "email":
                if (!IsValidEmail(fieldValue))
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون بريد إلكتروني صحيح");
                }
                break;

            case "url":
                if (!Uri.TryCreate(fieldValue, UriKind.Absolute, out _))
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون رابط صحيح");
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
                    return ResultDto<Unit>.Failure($"قيمة الحقل '{field.DisplayName}' يجب أن تكون رقم هاتف صحيح");
                }
                break;
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }

    /// <summary>
    /// التحقق من خيارات حقول الاختيار
    /// Validate select field options
    /// </summary>
    private ResultDto<Unit> ValidateSelectOptions(string fieldValue, UnitTypeField field, bool isMultiSelect)
    {
        if (string.IsNullOrWhiteSpace(field.FieldOptions))
        {
            return ResultDto<Unit>.Failure($"خيارات الحقل '{field.DisplayName}' غير محددة");
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
                        return ResultDto<Unit>.Failure($"القيمة '{value}' غير متوفرة في خيارات الحقل '{field.DisplayName}'");
                    }
                }
            }
            else
            {
                // للحقول أحادية الاختيار
                if (!availableOptions.Contains(fieldValue))
                {
                    return ResultDto<Unit>.Failure($"القيمة '{fieldValue}' غير متوفرة في خيارات الحقل '{field.DisplayName}'");
                }
            }
        }
        catch (JsonException)
        {
            _logger.LogWarning("خطأ في تحليل خيارات الحقل: {FieldId}", field.Id);
            return ResultDto<Unit>.Failure($"خيارات الحقل '{field.DisplayName}' غير صحيحة");
        }

        return ResultDto<Unit>.Ok(Unit.Value);
    }

    /// <summary>
    /// التحقق من قواعد التحقق المخصصة
    /// Validate custom validation rules
    /// </summary>
    private ResultDto<Unit> ValidateCustomRules(string fieldValue, string validationRules)
    {
        try
        {
            var rules = JsonSerializer.Deserialize<Dictionary<string, object>>(validationRules);
            
            if (rules == null)
            {
                return ResultDto<Unit>.Ok(Unit.Value);
            }

            // التحقق من الحد الأدنى والأقصى للطول
            if (rules.ContainsKey("minLength") && int.TryParse(rules["minLength"]?.ToString(), out int minLength))
            {
                if (fieldValue.Length < minLength)
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل يجب أن تكون على الأقل {minLength} أحرف");
                }
            }

            if (rules.ContainsKey("maxLength") && int.TryParse(rules["maxLength"]?.ToString(), out int maxLength))
            {
                if (fieldValue.Length > maxLength)
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل يجب أن تكون أقل من {maxLength} أحرف");
                }
            }

            // التحقق من الحد الأدنى والأقصى للقيم الرقمية
            if (rules.ContainsKey("min") && decimal.TryParse(fieldValue, out decimal numericValue))
            {
                if (decimal.TryParse(rules["min"]?.ToString(), out decimal minValue) && numericValue < minValue)
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل يجب أن تكون على الأقل {minValue}");
                }
            }

            if (rules.ContainsKey("max") && decimal.TryParse(fieldValue, out decimal numericValue2))
            {
                if (decimal.TryParse(rules["max"]?.ToString(), out decimal maxValue) && numericValue2 > maxValue)
                {
                    return ResultDto<Unit>.Failure($"قيمة الحقل يجب أن تكون أقل من {maxValue}");
                }
            }

            // التحقق من النمط (Pattern/Regex)
            if (rules.ContainsKey("pattern"))
            {
                var pattern = rules["pattern"]?.ToString();
                if (!string.IsNullOrEmpty(pattern) && !System.Text.RegularExpressions.Regex.IsMatch(fieldValue, pattern))
                {
                    return ResultDto<Unit>.Failure("قيمة الحقل لا تطابق النمط المطلوب");
                }
            }
        }
        catch (JsonException)
        {
            _logger.LogWarning("خطأ في تحليل قواعد التحقق المخصصة");
        }

        return ResultDto<Unit>.Ok(Unit.Value);
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
/// حدث تحديث قيمة حقل الوحدة
/// Unit field value updated event
/// </summary>
public class UnitFieldValueUpdatedEvent
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
    /// القيمة القديمة للحقل
    /// Old field value
    /// </summary>
    public string OldFieldValue { get; set; } = string.Empty;

    /// <summary>
    /// القيمة الجديدة للحقل
    /// New field value
    /// </summary>
    public string NewFieldValue { get; set; } = string.Empty;

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

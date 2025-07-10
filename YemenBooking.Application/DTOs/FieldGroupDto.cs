namespace YemenBooking.Application.DTOs;

/// <summary>
/// بيانات نقل مجموعة الحقول
/// DTO for FieldGroup entity
/// </summary>
public class FieldGroupDto
{
    /// <summary>
    /// معرف المجموعة
    /// GroupId
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    /// معرف نوع العقار
    /// PropertyTypeId
    /// </summary>
    public string PropertyTypeId { get; set; }

    /// <summary>
    /// اسم المجموعة
    /// GroupName
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// الاسم المعروض للمجموعة
    /// DisplayName
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// وصف المجموعة
    /// Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// ترتيب العرض
    /// SortOrder
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// هل المجموعة قابلة للطي
    /// IsCollapsible
    /// </summary>
    public bool IsCollapsible { get; set; }

    /// <summary>
    /// هل تكون موسعة افتراضياً
    /// IsExpandedByDefault
    /// </summary>
    public bool IsExpandedByDefault { get; set; }
} 
using YemenBooking.Core.Interfaces.Services;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان سجل التدقيق
/// Audit log entity
/// </summary>
public class AuditLog : BaseEntity
{
    /// <summary>
    /// نوع الكيان
    /// Entity type
    /// </summary>
    public string EntityType { get; set; } = null!;

    /// <summary>
    /// معرف الكيان
    /// Entity ID
    /// </summary>
    public Guid? EntityId { get; set; }

    /// <summary>
    /// الإجراء المتخذ
    /// Action performed
    /// </summary>
    public AuditAction Action { get; set; }

    /// <summary>
    /// القيم السابقة (JSON)
    /// Previous values (JSON)
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// القيم الجديدة (JSON)
    /// New values (JSON)
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// معرف المستخدم الذي قام بالعملية
    /// User who performed the action
    /// </summary>
    public Guid? PerformedBy { get; set; }

    /// <summary>
    /// اسم المستخدم
    /// Username
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// عنوان IP
    /// IP Address
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// وكيل المستخدم
    /// User agent
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// ملاحظات إضافية
    /// Additional notes
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// معلومات إضافية (JSON)
    /// Additional metadata (JSON)
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// حالة العملية
    /// Operation status
    /// </summary>
    public bool IsSuccessful { get; set; } = true;

    /// <summary>
    /// رسالة الخطأ في حالة الفشل
    /// Error message if failed
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// مدة العملية بالمللي ثانية
    /// Operation duration in milliseconds
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// معرف الجلسة
    /// Session ID
    /// </summary>
    public string? SessionId { get; set; }

    /// <summary>
    /// معرف الطلب
    /// Request ID
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// المصدر
    /// Source
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// المستخدم الذي قام بالعملية
    /// User who performed the action
    /// </summary>
    public virtual User? PerformedByUser { get; set; }

    // Helper Methods

    /// <summary>
    /// إضافة معلومات إضافية
    /// Add metadata
    /// </summary>
    public void AddMetadata(string key, object value)
    {
        var metadata = string.IsNullOrEmpty(Metadata) 
            ? new Dictionary<string, object>() 
            : System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(Metadata) ?? new Dictionary<string, object>();
        
        metadata[key] = value;
        Metadata = System.Text.Json.JsonSerializer.Serialize(metadata);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// الحصول على المعلومات الإضافية
    /// Get metadata
    /// </summary>
    public Dictionary<string, object>? GetMetadata()
    {
        if (string.IsNullOrEmpty(Metadata))
            return null;

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(Metadata);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// الحصول على القيم السابقة
    /// Get previous values
    /// </summary>
    public Dictionary<string, object>? GetOldValues()
    {
        if (string.IsNullOrEmpty(OldValues))
            return null;

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(OldValues);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// الحصول على القيم الجديدة
    /// Get new values
    /// </summary>
    public Dictionary<string, object>? GetNewValues()
    {
        if (string.IsNullOrEmpty(NewValues))
            return null;

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(NewValues);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// هل العملية فاشلة
    /// Is operation failed
    /// </summary>
    public bool IsFailed => !IsSuccessful;

    /// <summary>
    /// هل العملية استغرقت وقتاً طويلاً
    /// Is operation slow
    /// </summary>
    public bool IsSlowOperation => DurationMs.HasValue && DurationMs.Value > 5000; // أكثر من 5 ثوان
}

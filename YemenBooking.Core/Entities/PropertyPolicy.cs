namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان سياسة العقار
/// Property Policy entity
/// </summary>
public class PropertyPolicy : BaseEntity
{
    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// نوع السياسة (إلغاء، تعديل، دخول، أطفال، حيوانات)
    /// Policy type (Cancellation, Modification, CheckIn, Children, Pets)
    /// </summary>
    public PolicyType Type { get; set; }
    
    /// <summary>
    /// عدد أيام نافذة الإلغاء قبل تاريخ الوصول
    /// Number of days before check-in to allow cancellation
    /// </summary>
    public int CancellationWindowDays { get; set; }
    
    /// <summary>
    /// يتطلب الدفع الكامل قبل التأكيد
    /// Requires full payment before confirmation
    /// </summary>
    public bool RequireFullPaymentBeforeConfirmation { get; set; }
    
    /// <summary>
    /// الحد الأدنى لنسبة الدفع المقدمة (كنسبة مئوية)
    /// Minimum deposit percentage (as percentage)
    /// </summary>
    public decimal MinimumDepositPercentage { get; set; }
    
    /// <summary>
    /// الحد الأدنى للساعات قبل تسجيل الوصول لتعديل الحجز
    /// Minimum hours before check-in to allow modification
    /// </summary>
    public int MinHoursBeforeCheckIn { get; set; }
    
    /// <summary>
    /// وصف السياسة
    /// Policy description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// قواعد السياسة (JSON)
    /// Policy rules (JSON)
    /// </summary>
    public string Rules { get; set; }
    
    /// <summary>
    /// العقار المرتبط بالسياسة
    /// Property associated with the policy
    /// </summary>
    public virtual Property Property { get; set; }
}
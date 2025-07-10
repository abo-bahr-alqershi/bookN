namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان المستخدم
/// User entity
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// اسم المستخدم
    /// User name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// البريد الإلكتروني للمستخدم
    /// User email
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// كلمة المرور للمستخدم
    /// User password
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// رقم هاتف المستخدم
    /// User phone number
    /// </summary>
    public string Phone { get; set; }
    
    /// <summary>
    /// صورة المستخدم
    /// User name
    /// </summary>
    public string ProfileImage { get; set; }

    /// <summary>
    /// تاريخ إنشاء حساب المستخدم
    /// User account creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// حالة تفعيل الحساب
    /// Account activation status
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// تاريخ آخر تسجيل دخول
    /// Last login date
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    /// <summary>
    /// إجمالي المبلغ المنفق
    /// Total amount spent
    /// </summary>
    public decimal TotalSpent { get; set; } = 0;

    /// <summary>
    /// فئة الولاء (برونزي، فضي، ذهبي)
    /// Loyalty tier (Bronze, Silver, Gold)
    /// </summary>
    public string? LoyaltyTier { get; set; }
    
    /// <summary>
    /// الأدوار المرتبطة بالمستخدم
    /// Roles associated with the user
    /// </summary>
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
    /// <summary>
    /// العقارات المملوكة من قبل المستخدم
    /// Properties owned by the user
    /// </summary>
    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    
    /// <summary>
    /// الحجوزات التي قام بها المستخدم
    /// Bookings made by the user
    /// </summary>
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    
    /// <summary>
    /// الوظائف التي يشغلها المستخدم كموظف
    /// Staff positions held by the user
    /// </summary>
    public virtual ICollection<Staff> StaffPositions { get; set; } = new List<Staff>();

    /// <summary>
    /// هل تم تأكيد البريد الإلكتروني
    /// Email confirmed status
    /// </summary>
    public bool EmailConfirmed { get; set; } = false;

    /// <summary>
    /// رمز تأكيد البريد الإلكتروني
    /// Email confirmation token
    /// </summary>
    public string? EmailConfirmationToken { get; set; }

    /// <summary>
    /// تاريخ انتهاء صلاحية رمز تأكيد البريد الإلكتروني
    /// Expiration date of the email confirmation token
    /// </summary>
    public DateTime? EmailConfirmationTokenExpires { get; set; }

    /// <summary>
    /// رمز إعادة تعيين كلمة المرور
    /// Password reset token
    /// </summary>
    public string? PasswordResetToken { get; set; }

    /// <summary>
    /// تاريخ انتهاء صلاحية رمز إعادة تعيين كلمة المرور
    /// Expiration date of the password reset token
    /// </summary>
    public DateTime? PasswordResetTokenExpires { get; set; }

    /// <summary>
    /// إعدادات المستخدم بصيغة JSON
    /// User settings in JSON format
    /// </summary>
    public string SettingsJson { get; set; } = "{}";

    /// <summary>
    /// قائمة المفضلة للمستخدم بصيغة JSON
    /// User favorites list in JSON format
    /// </summary>
    public string FavoritesJson { get; set; } = "[]";

    /// <summary>
    /// البلاغات التي قام بها المستخدم
    /// Reports filed by the user
    /// </summary>
    public virtual ICollection<Report> ReportsMade { get; set; } = new List<Report>();

    /// <summary>
    /// البلاغات المقدمة ضد المستخدم
    /// Reports filed against the user
    /// </summary>
    public virtual ICollection<Report> ReportsAgainstUser { get; set; } = new List<Report>();
} 
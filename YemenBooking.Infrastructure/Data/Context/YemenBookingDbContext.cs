using Microsoft.EntityFrameworkCore;
using YemenBooking.Core.Entities;
using YemenBooking.Infrastructure.Data.Configurations;

namespace YemenBooking.Infrastructure.Data.Context;

/// <summary>
/// سياق قاعدة البيانات الرئيسي لنظام حجوزات اليمن
/// Main database context for Yemen Booking system
/// </summary>
public class YemenBookingDbContext : DbContext
{
    public YemenBookingDbContext(DbContextOptions<YemenBookingDbContext> options) : base(options)
    {
    }

    #region DbSets

    /// <summary>
    /// جدول المستخدمين
    /// Users table
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// جدول الأدوار
    /// Roles table
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// جدول أدوار المستخدمين
    /// User roles table
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; }

    /// <summary>
    /// جدول أنواع العقارات
    /// Property types table
    /// </summary>
    public DbSet<PropertyType> PropertyTypes { get; set; }

    /// <summary>
    /// جدول العقارات
    /// Properties table
    /// </summary>
    public DbSet<Property> Properties { get; set; }

    /// <summary>
    /// جدول صور العقارات
    /// Property images table
    /// </summary>
    public DbSet<PropertyImage> PropertyImages { get; set; }

    /// <summary>
    /// جدول أنواع الوحدات
    /// Unit types table
    /// </summary>
    public DbSet<UnitType> UnitTypes { get; set; }

    /// <summary>
    /// جدول الوحدات
    /// Units table
    /// </summary>
    public DbSet<Unit> Units { get; set; }

    /// <summary>
    /// جدول الحجوزات
    /// Bookings table
    /// </summary>
    public DbSet<Booking> Bookings { get; set; }

    /// <summary>
    /// جدول المدفوعات
    /// Payments table
    /// </summary>
    public DbSet<Payment> Payments { get; set; }

    /// <summary>
    /// جدول خدمات العقارات
    /// Property services table
    /// </summary>
    public DbSet<PropertyService> PropertyServices { get; set; }

    /// <summary>
    /// جدول خدمات الحجوزات
    /// Booking services table
    /// </summary>
    public DbSet<BookingService> BookingServices { get; set; }

    /// <summary>
    /// جدول المرافق
    /// Amenities table
    /// </summary>
    public DbSet<Amenity> Amenities { get; set; }

    /// <summary>
    /// جدول مرافق أنواع العقارات
    /// Property type amenities table
    /// </summary>
    public DbSet<PropertyTypeAmenity> PropertyTypeAmenities { get; set; }

    /// <summary>
    /// جدول مرافق العقارات
    /// Property amenities table
    /// </summary>
    public DbSet<PropertyAmenity> PropertyAmenities { get; set; }

    /// <summary>
    /// جدول التقييمات
    /// Reviews table
    /// </summary>
    public DbSet<Review> Reviews { get; set; }

    /// <summary>
    /// جدول سياسات العقارات
    /// Property policies table
    /// </summary>
    public DbSet<PropertyPolicy> PropertyPolicies { get; set; }

    /// <summary>
    /// جدول الموظفين
    /// Staff table
    /// </summary>
    public DbSet<Staff> Staff { get; set; }

    /// <summary>
    /// جدول إجراءات الإدارة
    /// Admin actions table
    /// </summary>
    public DbSet<AdminAction> AdminActions { get; set; }

    /// <summary>
    /// جدول الإشعارات
    /// Notifications table
    /// </summary>
    public DbSet<Notification> Notifications { get; set; }

    /// <summary>
    /// جدول سجلات التدقيق
    /// Audit logs table
    /// </summary>
    public DbSet<AuditLog> AuditLogs { get; set; }

    /// <summary>
    /// جدول صور التقييمات
    /// Review images table
    /// </summary>
    public DbSet<ReviewImage> ReviewImages { get; set; }

    /// <summary>
    /// جدول البلاغات
    /// Reports table
    /// </summary>
    public DbSet<Report> Reports { get; set; }

    /// <summary>
    /// جدول أنواع الحقول
    /// Field types table
    /// </summary>
    public DbSet<FieldType> FieldTypes { get; set; }

    /// <summary>
    /// جدول حقول أنواع العقارات
    /// Property type fields table
    /// </summary>
    public DbSet<UnitTypeField> UnitTypeFields { get; set; }

    /// <summary>
    /// جدول مجموعات الحقول
    /// Field groups table
    /// </summary>
    public DbSet<FieldGroup> FieldGroups { get; set; }

    /// <summary>
    /// جدول ارتباط الحقول بالمجموعات
    /// Field group fields table
    /// </summary>
    public DbSet<FieldGroupField> FieldGroupFields { get; set; }

    /// <summary>
    /// جدول الفلاتر
    /// Search filters table
    /// </summary>
    public DbSet<SearchFilter> SearchFilters { get; set; }

    /// <summary>
    /// جدول قيم الحقول للوحدات
    /// Unit field values table
    /// </summary>
    public DbSet<UnitFieldValue> UnitFieldValues { get; set; }


    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // تطبيق جميع إعدادات الكيانات
        // Apply all entity configurations
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyImageConfiguration());
        modelBuilder.ApplyConfiguration(new UnitTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UnitConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyServiceConfiguration());
        modelBuilder.ApplyConfiguration(new BookingServiceConfiguration());
        modelBuilder.ApplyConfiguration(new AmenityConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyTypeAmenityConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyAmenityConfiguration());
        modelBuilder.ApplyConfiguration(new FieldTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UnitTypeFieldConfiguration());
        modelBuilder.ApplyConfiguration(new FieldGroupConfiguration());
        modelBuilder.ApplyConfiguration(new FieldGroupFieldConfiguration());
        modelBuilder.ApplyConfiguration(new SearchFilterConfiguration());
        modelBuilder.ApplyConfiguration(new UnitFieldValueConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyPolicyConfiguration());
        modelBuilder.ApplyConfiguration(new StaffConfiguration());
        modelBuilder.ApplyConfiguration(new AdminActionConfiguration());

        // Configurations for new entities
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewImageConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
    }
}

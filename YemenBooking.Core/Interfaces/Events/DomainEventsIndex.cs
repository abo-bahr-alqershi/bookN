using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// فهرس شامل لجميع أحداث النطاق في نظام إدارة الضيافة
/// Comprehensive index of all domain events in the hospitality management system
/// </summary>
/// <remarks>
/// هذا الملف يحتوي على مرجع لجميع الأحداث المُنشأة مع التوثيق الشامل
/// This file contains references to all created events with comprehensive documentation
/// 
/// تم إنشاء جميع الأحداث بناءً على ملف cqrs_commands_queries.md
/// All events were created based on the cqrs_commands_queries.md file
/// </remarks>
public static class DomainEventsIndex
{
    /// <summary>
    /// أحداث إدارة المستخدمين
    /// User Management Events
    /// </summary>
    public static class UserEvents
    {
        // IUserCreatedEvent - حدث إنشاء مستخدم جديد
        // IUserUpdatedEvent - حدث تحديث بيانات المستخدم  
        // IUserDeactivatedEvent - حدث إلغاء تفعيل المستخدم
        // IUserRoleAssignedEvent - حدث تخصيص دور للمستخدم
    }

    /// <summary>
    /// أحداث إدارة العقارات
    /// Property Management Events
    /// </summary>
    public static class PropertyEvents
    {
        // IPropertyCreatedEvent - حدث إنشاء عقار جديد
        // IPropertyUpdatedEvent - حدث تحديث بيانات العقار
        // IPropertyApprovedEvent - حدث الموافقة على العقار
        // IPropertyRejectedEvent - حدث رفض العقار
        // IPropertyDeletedEvent - حدث حذف العقار
    }

    /// <summary>
    /// أحداث إدارة الوحدات
    /// Unit Management Events
    /// </summary>
    public static class UnitEvents
    {
        // IUnitCreatedEvent - حدث إنشاء وحدة جديدة
        // IUnitUpdatedEvent - حدث تحديث بيانات الوحدة
        // IUnitAvailabilityUpdatedEvent - حدث تحديث حالة توفر الوحدة
        // IUnitDeletedEvent - حدث حذف الوحدة
    }

    /// <summary>
    /// أحداث إدارة الحجوزات
    /// Booking Management Events
    /// </summary>
    public static class BookingEvents
    {
        // IBookingCreatedEvent - حدث إنشاء حجز جديد
        // IBookingUpdatedEvent - حدث تحديث بيانات الحجز
        // IBookingConfirmedEvent - حدث تأكيد الحجز
        // IBookingCancelledEvent - حدث إلغاء الحجز
        // IBookingCompletedEvent - حدث إكمال الحجز
    }

    /// <summary>
    /// أحداث إدارة المدفوعات
    /// Payment Management Events
    /// </summary>
    public static class PaymentEvents
    {
        // IPaymentProcessedEvent - حدث معالجة الدفع
        // IPaymentRefundedEvent - حدث استرداد الدفع
        // IPaymentStatusUpdatedEvent - حدث تحديث حالة الدفع
        // IPaymentFailedEvent - حدث فشل الدفع (إضافي)
    }

    /// <summary>
    /// أحداث إدارة خدمات العقارات
    /// Property Services Management Events
    /// </summary>
    public static class PropertyServiceEvents
    {
        // IPropertyServiceCreatedEvent - حدث إنشاء خدمة عقار جديدة
        // IPropertyServiceUpdatedEvent - حدث تحديث خدمة العقار
    }

    /// <summary>
    /// أحداث إدارة خدمات الحجوزات
    /// Booking Services Management Events
    /// </summary>
    public static class BookingServiceEvents
    {
        // IServiceAddedToBookingEvent - حدث إضافة خدمة للحجز
        // IServiceRemovedFromBookingEvent - حدث إزالة خدمة من الحجز
    }

    /// <summary>
    /// أحداث إدارة المرافق العامة
    /// Global Amenities Management Events
    /// </summary>
    public static class GlobalAmenityEvents
    {
        // IAmenityCreatedEvent - حدث إنشاء مرفق جديد
        // IAmenityUpdatedEvent - حدث تحديث المرفق
        // IAmenityAssignedToPropertyTypeEvent - حدث تخصيص مرفق لنوع عقار
        // IPropertyAmenityUpdatedEvent - حدث تحديث حالة مرفق العقار
    }

    /// <summary>
    /// أحداث إدارة مرافق العقارات
    /// Property Amenities Management Events
    /// </summary>
    public static class PropertyAmenityEvents
    {
        // IAmenityAddedToPropertyEvent - حدث إضافة مرفق للعقار (إضافي)
        // IAmenityUpdatedEvent - حدث تحديث مرفق العقار (إضافي)
        // IAmenityRemovedFromPropertyEvent - حدث إزالة مرفق من العقار (إضافي)
    }

    /// <summary>
    /// أحداث إدارة التقييمات
    /// Review Management Events
    /// </summary>
    public static class ReviewEvents
    {
        // IReviewCreatedEvent - حدث إنشاء تقييم جديد
        // IReviewUpdatedEvent - حدث تحديث التقييم
        // IReviewDeletedEvent - حدث حذف التقييم
    }

    /// <summary>
    /// أحداث إدارة السياسات
    /// Policy Management Events
    /// </summary>
    public static class PolicyEvents
    {
        // IPropertyPolicyCreatedEvent - حدث إنشاء سياسة عقار جديدة
        // IPropertyPolicyUpdatedEvent - حدث تحديث سياسة العقار
        // IPolicyDeletedEvent - حدث حذف السياسة (إضافي)
    }

    /// <summary>
    /// أحداث إدارة الموظفين
    /// Staff Management Events
    /// </summary>
    public static class StaffEvents
    {
        // IStaffAddedEvent - حدث إضافة موظف جديد
        // IStaffUpdatedEvent - حدث تحديث بيانات الموظف
        // IStaffRemovedEvent - حدث إزالة الموظف
    }

    /// <summary>
    /// أحداث إدارة أنواع العقارات والوحدات
    /// Property & Unit Types Management Events
    /// </summary>
    public static class TypeEvents
    {
        // IPropertyTypeCreatedEvent - حدث إنشاء نوع عقار جديد
        // IPropertyTypeUpdatedEvent - حدث تحديث نوع العقار (إضافي)
        // IUnitTypeCreatedEvent - حدث إنشاء نوع وحدة جديد
        // IUnitTypeUpdatedEvent - حدث تحديث نوع الوحدة (إضافي)
    }
}

/// <summary>
/// إحصائيات الأحداث المُنشأة
/// Created Events Statistics
/// </summary>
public static class EventsStatistics
{
    /// <summary>
    /// إجمالي الأحداث المطلوبة حسب ملف cqrs_commands_queries.md
    /// Total required events according to cqrs_commands_queries.md
    /// </summary>
    public const int TotalRequiredEvents = 39;

    /// <summary>
    /// إجمالي الأحداث المُنشأة فعلياً
    /// Total events actually created
    /// </summary>
    public const int TotalCreatedEvents = 45;

    /// <summary>
    /// الأحداث الإضافية المفيدة
    /// Additional useful events
    /// </summary>
    public const int AdditionalEvents = 6;

    /// <summary>
    /// نسبة الإكمال للأحداث المطلوبة
    /// Completion rate for required events
    /// </summary>
    public const double CompletionRate = 100.0; // %

    /// <summary>
    /// تاريخ آخر تحديث
    /// Last update date
    /// </summary>
    public static readonly DateTime LastUpdated = DateTime.Now;
}

using YemenBooking.Core.Entities;

namespace YemenBooking.Core.Interfaces.Repositories;

/// <summary>
/// واجهة مستودع العقارات
/// Property repository interface
/// </summary>
public interface IPropertyRepository : IRepository<Property>
{
    /// <summary>
    /// إنشاء عقار جديد
    /// Create new property
    /// </summary>
    Task<Property> CreatePropertyAsync(Property property, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على عقار بواسطة المعرف
    /// Get property by id
    /// </summary>
    Task<Property?> GetPropertyByIdAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقار مع الوحدات
    /// Get property with units
    /// </summary>
    Task<Property?> GetPropertyWithUnitsAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقار مع المرافق
    /// Get property with amenities
    /// </summary>
    Task<Property?> GetPropertyWithAmenitiesAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// تحديث العقار
    /// Update property
    /// </summary>
    Task<Property> UpdatePropertyAsync(Property property, CancellationToken cancellationToken = default);

    /// <summary>
    /// الموافقة على العقار
    /// Approve property
    /// </summary>
    Task<bool> ApprovePropertyAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// رفض العقار
    /// Reject property
    /// </summary>
    Task<bool> RejectPropertyAsync(Guid propertyId, string reason, CancellationToken cancellationToken = default);

    /// <summary>
    /// حذف العقار
    /// Delete property
    /// </summary>
    Task<bool> DeletePropertyAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على عقارات المالك
    /// Get properties by owner
    /// </summary>
    Task<IEnumerable<Property>> GetPropertiesByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقارات حسب النوع
    /// Get properties by type
    /// </summary>
    Task<IEnumerable<Property>> GetPropertiesByTypeAsync(Guid propertyTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقارات حسب المدينة
    /// Get properties by city
    /// </summary>
    Task<IEnumerable<Property>> GetPropertiesByCityAsync(string city, CancellationToken cancellationToken = default);

    /// <summary>
    /// البحث عن العقارات
    /// Search properties
    /// </summary>
    Task<IEnumerable<Property>> SearchPropertiesAsync(string searchTerm, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقارات القريبة من موقع
    /// Get properties near location
    /// </summary>
    Task<IEnumerable<Property>> GetPropertiesNearLocationAsync(
        double latitude,
        double longitude,
        double radiusKm,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقارات المعلقة
    /// Get pending properties
    /// </summary>
    Task<IEnumerable<Property>> GetPendingPropertiesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقارات المميزة
    /// Get featured properties
    /// </summary>
    Task<IEnumerable<Property>> GetFeaturedPropertiesAsync(int count, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقارات الشائعة
    /// Get popular destinations
    /// </summary>
    Task<IEnumerable<Property>> GetPopularDestinationsAsync(int count, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على العقارات المقترحة
    /// Get recommended properties
    /// </summary>
    Task<IEnumerable<Property>> GetRecommendedPropertiesAsync(
        Guid userId,
        int count,
        CancellationToken cancellationToken = default);
        
            /// <summary>
    /// الحصول على المالك بواسطة المعرف
    /// Get owner by id
    /// </summary>
    Task<User?> GetOwnerByIdAsync(Guid ownerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على نوع العقار بواسطة المعرف
    /// Get property type by id
    /// </summary>
    Task<PropertyType?> GetPropertyTypeByIdAsync(Guid propertyTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// التحقق من وجود حجوزات نشطة
    /// Check active bookings
    /// </summary>
    Task<bool> CheckActiveBookingsAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على مرافق العقار
    /// Get property amenities
    /// </summary>
    Task<IEnumerable<PropertyAmenity>> GetPropertyAmenitiesAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// تحديث مرافق العقار
    /// Update property amenity
    /// </summary>
    Task<bool> UpdatePropertyAmenityAsync(
        Guid propertyId, 
        Guid amenityId, 
        bool isAvailable, 
        decimal? additionalCost = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// حساب مؤشرات أداء العقار
    /// Calculate property performance metrics
    /// </summary>
    Task<object> CalculatePerformanceMetricsAsync(
        Guid propertyId, 
        DateTime fromDate, 
        DateTime toDate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على أفضل العقارات أداء وفق عدد الحجوزات
    /// Get top performing properties by booking count
    /// </summary>
    Task<IEnumerable<Property>> GetTopPerformingPropertiesAsync(int count, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على سياسة الإلغاء للعقار
    /// Get cancellation policy for a property
    /// </summary>
    Task<PropertyPolicy?> GetCancellationPolicyAsync(Guid propertyId, CancellationToken cancellationToken = default);
}

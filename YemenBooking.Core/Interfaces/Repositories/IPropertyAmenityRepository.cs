using YemenBooking.Core.Entities;

namespace YemenBooking.Core.Interfaces.Repositories;

/// <summary>
/// واجهة مستودع مرافق العقارات
/// Property amenity repository interface
/// </summary>
public interface IPropertyAmenityRepository : IRepository<PropertyAmenity>
{
    /// <summary>
    /// تحديث مرفق العقار
    /// Update property amenity
    /// </summary>
    Task<PropertyAmenity> UpdatePropertyAmenityAsync(PropertyAmenity propertyAmenity, CancellationToken cancellationToken = default);

    /// <summary>
    /// إضافة مرفق للعقار
    /// Add amenity to property
    /// </summary>
    Task<PropertyAmenity> AddAmenityToPropertyAsync(PropertyAmenity propertyAmenity, CancellationToken cancellationToken = default);

    /// <summary>
    /// إزالة مرفق من العقار
    /// Remove amenity from property
    /// </summary>
    Task<bool> RemoveAmenityFromPropertyAsync(Guid propertyId, Guid amenityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على مرافق العقار
    /// Get property amenities
    /// </summary>
    Task<IEnumerable<PropertyAmenity>> GetPropertyAmenitiesAsync(Guid propertyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على مرافق العقار حسب العقار
    /// Get amenities by property
    /// </summary>
    Task<IEnumerable<PropertyAmenity>> GetAmenitiesByPropertyAsync(Guid propertyId, CancellationToken cancellationToken = default);
}

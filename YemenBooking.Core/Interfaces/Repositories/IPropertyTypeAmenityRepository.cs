using YemenBooking.Core.Entities;

namespace YemenBooking.Core.Interfaces.Repositories;

/// <summary>
/// واجهة مستودع مرافق أنواع العقارات
/// Property type amenity repository interface
/// </summary>
public interface IPropertyTypeAmenityRepository : IRepository<PropertyTypeAmenity>
{
    /// <summary>
    /// تخصيص مرفق لنوع العقار
    /// Assign amenity to property type
    /// </summary>
    Task<PropertyTypeAmenity> AssignAmenityToPropertyTypeAsync(PropertyTypeAmenity propertyTypeAmenity, CancellationToken cancellationToken = default);

    /// <summary>
    /// إزالة مرفق من نوع العقار
    /// Remove amenity from property type
    /// </summary>
    Task<bool> RemoveAmenityFromPropertyTypeAsync(Guid propertyTypeId, Guid amenityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على مرافق نوع العقار
    /// Get amenities by property type
    /// </summary>
    Task<IEnumerable<PropertyTypeAmenity>> GetAmenitiesByPropertyTypeAsync(Guid propertyTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على أنواع العقارات للمرفق
    /// Get property types by amenity
    /// </summary>
    Task<IEnumerable<PropertyTypeAmenity>> GetPropertyTypesByAmenityAsync(Guid amenityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// التحقق من وجود مرفق في نوع العقار
    /// Check if amenity exists in property type
    /// </summary>
    Task<bool> PropertyTypeHasAmenityAsync(Guid propertyTypeId, Guid amenityId, CancellationToken cancellationToken = default);
}

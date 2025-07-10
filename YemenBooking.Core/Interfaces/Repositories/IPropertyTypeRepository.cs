using YemenBooking.Core.Entities;

namespace YemenBooking.Core.Interfaces.Repositories;

/// <summary>
/// واجهة مستودع أنواع العقارات
/// Property type repository interface
/// </summary>
public interface IPropertyTypeRepository : IRepository<PropertyType>
{
    /// <summary>
    /// إنشاء نوع عقار جديد
    /// Create new property type
    /// </summary>
    Task<PropertyType> CreatePropertyTypeAsync(PropertyType propertyType, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على نوع العقار بواسطة المعرف
    /// Get property type by id
    /// </summary>
    Task<PropertyType?> GetPropertyTypeByIdAsync(Guid propertyTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على جميع أنواع العقارات
    /// Get all property types
    /// </summary>
    Task<IEnumerable<PropertyType>> GetAllPropertyTypesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على نوع العقار مع المرافق
    /// Get property type with amenities
    /// </summary>
    Task<PropertyType?> GetPropertyTypeWithAmenitiesAsync(Guid propertyTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// تحديث نوع العقار
    /// Update property type
    /// </summary>
    Task<PropertyType> UpdatePropertyTypeAsync(PropertyType propertyType, CancellationToken cancellationToken = default);

    /// <summary>
    /// حذف نوع العقار
    /// Delete property type
    /// </summary>
    Task<bool> DeletePropertyTypeAsync(Guid propertyTypeId, CancellationToken cancellationToken = default);
}

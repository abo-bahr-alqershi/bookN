using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YemenBooking.Core.Entities;

namespace YemenBooking.Core.Interfaces.Repositories;

/// <summary>
/// واجهة مستودع أنواع الحقول
/// FieldType repository interface
/// </summary>
public interface IFieldTypeRepository : IRepository<FieldType>
{
    /// <summary>
    /// إنشاء نوع حقل جديد
    /// Create new field type
    /// </summary>
    Task<FieldType> CreateFieldTypeAsync(FieldType fieldType, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على نوع الحقل بواسطة المعرف
    /// Get field type by id
    /// </summary>
    Task<FieldType?> GetFieldTypeByIdAsync(Guid fieldTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// تحديث نوع الحقل
    /// Update field type
    /// </summary>
    Task<FieldType> UpdateFieldTypeAsync(FieldType fieldType, CancellationToken cancellationToken = default);

    /// <summary>
    /// حذف نوع الحقل
    /// Delete field type
    /// </summary>
    Task<bool> DeleteFieldTypeAsync(Guid fieldTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// الحصول على جميع أنواع الحقول
    /// Get all field types
    /// </summary>
    Task<IEnumerable<FieldType>> GetAllFieldTypesAsync(CancellationToken cancellationToken = default);
} 
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Infrastructure.Data.Context;

namespace YemenBooking.Infrastructure.Repositories;

/// <summary>
/// تنفيذ مستودع أنواع الحقول
/// FieldType repository implementation
/// </summary>
public class FieldTypeRepository : BaseRepository<FieldType>, IFieldTypeRepository
{
    public FieldTypeRepository(YemenBookingDbContext context) : base(context)
    {
    }

    public async Task<FieldType> CreateFieldTypeAsync(FieldType fieldType, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(fieldType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return fieldType;
    }

    public async Task<FieldType?> GetFieldTypeByIdAsync(Guid fieldTypeId, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object[] { fieldTypeId }, cancellationToken);

    public async Task<FieldType> UpdateFieldTypeAsync(FieldType fieldType, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(fieldType);
        await _context.SaveChangesAsync(cancellationToken);
        return fieldType;
    }

    public async Task<bool> DeleteFieldTypeAsync(Guid fieldTypeId, CancellationToken cancellationToken = default)
    {
        var entity = await GetFieldTypeByIdAsync(fieldTypeId, cancellationToken);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<FieldType>> GetAllFieldTypesAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);
} 
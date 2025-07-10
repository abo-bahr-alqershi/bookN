using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Infrastructure.Data.Context;

namespace YemenBooking.Infrastructure.Repositories
{
    /// <summary>
    /// تنفيذ مستودع توفر الوحدات
    /// Unit availability repository implementation
    /// </summary>
    public class UnitAvailabilityRepository : BaseRepository<Unit>, IUnitAvailabilityRepository
    {
        public UnitAvailabilityRepository(YemenBookingDbContext context) : base(context) { }

        public async Task<bool> UpdateAvailabilityAsync(Guid unitId, DateTime fromDate, DateTime toDate, bool isAvailable, CancellationToken cancellationToken = default)
        {
            // تحديث حالة التوفر العام للوحدة
            var unit = await GetByIdAsync(unitId, cancellationToken);
            if (unit == null) return false;
            unit.IsAvailable = isAvailable;
            _dbSet.Update(unit);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IDictionary<DateTime, bool>> GetUnitAvailabilityAsync(Guid unitId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            var dict = new Dictionary<DateTime, bool>();
            for (var date = fromDate.Date; date <= toDate.Date; date = date.AddDays(1))
            {
                var overlapping = await _context.Bookings.AnyAsync(b => b.UnitId == unitId && b.CheckIn <= date && b.CheckOut > date, cancellationToken);
                dict[date] = !overlapping;
            }
            return dict;
        }

        public async Task<bool> IsUnitAvailableAsync(Guid unitId, DateTime checkIn, DateTime checkOut, CancellationToken cancellationToken = default)
        {
            var overlapping = await _context.Bookings.AnyAsync(b => b.UnitId == unitId && b.CheckIn < checkOut && b.CheckOut > checkIn, cancellationToken);
            return !overlapping;
        }

        public async Task<bool> BlockUnitPeriodAsync(Guid unitId, DateTime fromDate, DateTime toDate, string reason, CancellationToken cancellationToken = default)
        {
            // حجز الوحدة (تعطيل التوفر) خلال الفترة المحددة
            return await UpdateAvailabilityAsync(unitId, fromDate, toDate, false, cancellationToken);
        }

        public async Task<bool> UnblockUnitPeriodAsync(Guid unitId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            // إلغاء حجز الوحدة (تفعيل التوفر) خلال الفترة المحددة
            return await UpdateAvailabilityAsync(unitId, fromDate, toDate, true, cancellationToken);
        }
    }
} 
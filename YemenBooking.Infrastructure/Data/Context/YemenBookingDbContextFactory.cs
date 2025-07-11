using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using YemenBooking.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace YemenBooking.Infrastructure.Data.Context;

/// <summary>
/// Factory for design-time DbContext creation.
/// مولد سياق قاعدة البيانات في وقت التصميم
/// </summary>
public class YemenBookingDbContextFactory : IDesignTimeDbContextFactory<YemenBookingDbContext>
{
    public YemenBookingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<YemenBookingDbContext>();
        // تهيئة الاتصال بقاعدة بيانات SQLite
        optionsBuilder.UseSqlite("Data Source=YemenBooking.db")
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        return new YemenBookingDbContext(optionsBuilder.Options);
    }
} 
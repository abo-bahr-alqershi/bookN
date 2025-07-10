using System;
using MediatR;
using YemenBooking.Application.DTOs.Dashboard;

namespace YemenBooking.Application.Queries.Dashboard
{
    /// <summary>
    /// استعلام للحصول على نسبة الإشغال لعقار ضمن نطاق زمني
    /// Query to retrieve occupancy rate for a property within a date range
    /// </summary>
    public class GetOccupancyRateQuery : IRequest<decimal>
    {
        /// <summary>
        /// معرف العقار
        /// Property identifier
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// النطاق الزمني
        /// Date range for the calculation
        /// </summary>
        public DateRangeDto Range { get; set; }

        public GetOccupancyRateQuery(Guid propertyId, DateRangeDto range)
        {
            PropertyId = propertyId;
            Range = range;
        }
    }
} 
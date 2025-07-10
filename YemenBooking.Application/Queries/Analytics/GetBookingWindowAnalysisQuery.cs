using System;
using MediatR;
using YemenBooking.Application.DTOs.Analytics;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Analytics;

/// <summary>
/// استعلام للحصول على تحليل نافذة الحجز لعقار محدد
/// Query to get booking window analysis for a specific property
/// </summary>
public class GetBookingWindowAnalysisQuery : IRequest<ResultDto<BookingWindowDto>>
{
    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }

    public GetBookingWindowAnalysisQuery(Guid propertyId)
    {
        PropertyId = propertyId;
    }
} 
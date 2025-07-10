using System;
using System.Collections.Generic;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لبيانات المراجعة
    /// DTO for review data
    /// </summary>
    public class ReviewDto
    {
        /// <summary>
        /// معرف المراجعة
        /// Review identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// معرف الحجز
        /// Booking identifier
        /// </summary>
        public Guid BookingId { get; set; }

        /// <summary>
        /// تقييم النظافة
        /// Cleanliness rating
        /// </summary>
        public int Cleanliness { get; set; }

        /// <summary>
        /// تقييم الخدمة
        /// Service rating
        /// </summary>
        public int Service { get; set; }

        /// <summary>
        /// تقييم الموقع
        /// Location rating
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// تقييم القيمة
        /// Value rating
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// تعليق المراجعة
        /// Review comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// تاريخ إنشاء المراجعة
        /// Review creation date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// صور التقييم
        /// Review images
        /// </summary>
        public List<ReviewImageDto> Images { get; set; } = new List<ReviewImageDto>();
    }
} 
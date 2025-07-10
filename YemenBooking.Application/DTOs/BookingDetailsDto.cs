using System.Collections.Generic;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لتفاصيل الحجز
    /// DTO for booking details
    /// </summary>
    public class BookingDetailsDto : BookingDto
    {
        /// <summary>
        /// المدفوعات المرتبطة بالحجز
        /// Payments associated with the booking
        /// </summary>
        public IEnumerable<PaymentDto> Payments { get; set; }

        /// <summary>
        /// الخدمات المرتبطة بالحجز
        /// Services associated with the booking
        /// </summary>
        public IEnumerable<ServiceDto> Services { get; set; }
    }
} 
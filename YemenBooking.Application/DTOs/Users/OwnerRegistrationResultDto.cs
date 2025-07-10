using System;

namespace YemenBooking.Application.DTOs.Users
{
    /// <summary>
    /// نتيجة تسجيل مالك العقار
    /// Property owner registration result
    /// </summary>
    public class OwnerRegistrationResultDto
    {
        /// <summary>
        /// معرف المستخدم
        /// User identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// معرف العقار
        /// Property identifier
        /// </summary>
        public Guid PropertyId { get; set; }
    }
} 
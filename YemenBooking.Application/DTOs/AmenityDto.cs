using System;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لبيانات المرفق
    /// DTO for amenity data
    /// </summary>
    public class AmenityDto
    {
        /// <summary>
        /// معرف المرفق
        /// Amenity identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// اسم المرفق
        /// Amenity name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// وصف المرفق
        /// Amenity description
        /// </summary>
        public string Description { get; set; }
    }
} 
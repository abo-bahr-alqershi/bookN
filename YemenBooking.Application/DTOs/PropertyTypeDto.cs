using System;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لبيانات نوع العقار
    /// DTO for property type data
    /// </summary>
    public class PropertyTypeDto
    {
        /// <summary>
        /// معرف نوع العقار
        /// Property type identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// اسم نوع العقار
        /// Property type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// وصف نوع العقار
        /// Property type description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// المرافق الافتراضية (JSON)
        /// Default amenities (JSON)
        /// </summary>
        public string DefaultAmenities { get; set; }
    }
} 
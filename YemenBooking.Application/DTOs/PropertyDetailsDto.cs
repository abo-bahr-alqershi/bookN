using System.Collections.Generic;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لتفاصيل العقار
    /// DTO for property details
    /// </summary>
    public class PropertyDetailsDto : PropertyDto
    {
        /// <summary>
        /// قائمة الوحدات التابعة للعقار
        /// List of units for the property
        /// </summary>
        public IEnumerable<UnitDto> Units { get; set; }

        /// <summary>
        /// قائمة المرافق التابعة للعقار
        /// List of amenities for the property
        /// </summary>
        public IEnumerable<AmenityDto> Amenities { get; set; }

    }
} 
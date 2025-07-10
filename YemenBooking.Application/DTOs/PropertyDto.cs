using System;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لبيانات العقار
    /// DTO for property data
    /// </summary>
    public class PropertyDto
    {
        /// <summary>
        /// المعرف الفريد للعقار
        /// Property unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// معرف المالك
        /// Owner identifier
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// معرف نوع العقار
        /// Property type identifier
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// اسم العقار
        /// Property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// العنوان الكامل للعقار
        /// Full address of the property
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// المدينة
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// خط العرض
        /// Latitude
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// خط الطول
        /// Longitude
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// تقييم النجوم
        /// Star rating
        /// </summary>
        public int StarRating { get; set; }

        /// <summary>
        /// وصف العقار
        /// Property description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// حالة الموافقة على العقار
        /// Is approved status
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// تاريخ إنشاء العقار
        /// Creation date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// اسم المالك
        /// Name of the owner
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// اسم نوع العقار
        /// Property type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// المسافة من الموقع الحالي بالكيلومترات
        /// Distance from current location in kilometers
        /// </summary>
        public double? DistanceKm { get; set; }
    }
} 
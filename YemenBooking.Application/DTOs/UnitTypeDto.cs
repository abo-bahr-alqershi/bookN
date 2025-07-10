using System;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// تفاصيل نوع الوحدة
    /// Unit type DTO
    /// </summary>
    public class UnitTypeDto
    {
        /// <summary>
        /// معرف نوع الوحدة
        /// Unit type identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// معرف نوع العقار
        /// Property type identifier
        /// </summary>
        public Guid PropertyTypeId { get; set; }

        /// <summary>
        /// اسم نوع الوحدة
        /// Unit type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// وصف نوع الوحدة
        /// Unit type description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// قواعد التسعير الافتراضية (JSON)
        /// Default pricing rules (JSON)
        /// </summary>
        public string DefaultPricingRules { get; set; }
    }
} 
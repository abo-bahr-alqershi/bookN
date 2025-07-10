using YemenBooking.Core.Enums;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// تفاصيل الخدمة
    /// Service details DTO
    /// </summary>
    public class ServiceDetailsDto
    {
        /// <summary>
        /// معرف الخدمة
        /// Service identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// معرف العقار
        /// Property identifier
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// اسم العقار
        /// Property name
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// اسم الخدمة
        /// Service name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// سعر الخدمة
        /// Service price
        /// </summary>
        public MoneyDto Price { get; set; }

        /// <summary>
        /// نموذج التسعير
        /// Pricing model
        /// </summary>
        public PricingModel PricingModel { get; set; }
    }
} 
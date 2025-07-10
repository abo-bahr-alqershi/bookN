using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.PropertyTypes
{
    /// <summary>
    /// استعلام للحصول على نوع عقار محدد
    /// Query to get a specific property type by ID
    /// </summary>
    public class GetPropertyTypeByIdQuery : IRequest<ResultDto<PropertyTypeDto>>
    {
        /// <summary>
        /// معرف نوع العقار
        /// </summary>
        public Guid PropertyTypeId { get; set; }
    }
} 
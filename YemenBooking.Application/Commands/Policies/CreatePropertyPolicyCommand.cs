using System;
using MediatR;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Enums;

namespace YemenBooking.Application.Commands.Policies
{
    /// <summary>
    /// أمر لإنشاء سياسة جديدة للعقار
    /// Command to create a new property policy
    /// </summary>
    public class CreatePropertyPolicyCommand : IRequest<ResultDto<Guid>>
    {
        /// <summary>
        /// معرف العقار
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// نوع السياسة
        /// </summary>
        public PolicyType PolicyType { get; set; }

        /// <summary>
        /// الوصف
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// القواعد
        /// </summary>
        public string Rules { get; set; }
    }
} 
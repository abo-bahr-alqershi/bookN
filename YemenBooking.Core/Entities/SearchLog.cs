using System;

namespace YemenBooking.Core.Entities
{
    /// <summary>
    /// سجل عمليات البحث من قبل المستخدمين
    /// Search logs by users
    /// </summary>
    public class SearchLog : BaseEntity
    {
        /// <summary>
        /// معرف المستخدم الذي قام بالبحث
        /// User identifier who performed the search
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// نوع البحث (Property أو Unit)
        /// Search type (Property or Unit)
        /// </summary>
        public string SearchType { get; set; } = string.Empty;

        /// <summary>
        /// معايير البحث والفلترة بصيغة JSON
        /// Search and filter criteria in JSON format
        /// </summary>
        public string CriteriaJson { get; set; } = "{}";

        /// <summary>
        /// عدد النتائج المرجعة
        /// Number of results returned
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// رقم الصفحة
        /// Page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// حجم الصفحة
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
    }
} 
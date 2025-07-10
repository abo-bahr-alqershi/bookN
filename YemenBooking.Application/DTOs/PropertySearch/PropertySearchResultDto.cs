using System;
using System.Collections.Generic;

namespace YemenBooking.Application.DTOs.PropertySearch
{
    /// <summary>
    /// نتيجة البحث في العقارات
    /// </summary>
    public class PropertySearchResultDto
    {
        /// <summary>
        /// قائمة العقارات
        /// </summary>
        public List<PropertySearchItemDto> Properties { get; set; } = new List<PropertySearchItemDto>();

        /// <summary>
        /// العدد الإجمالي للنتائج
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// رقم الصفحة الحالية
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// حجم الصفحة
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// إجمالي عدد الصفحات
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// وجود صفحة سابقة
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// وجود صفحة تالية
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// الفلاتر المطبقة
        /// </summary>
        public List<SearchFilterCriteriaDto> AppliedFilters { get; set; } = new List<SearchFilterCriteriaDto>();

        /// <summary>
        /// إحصائيات البحث
        /// </summary>
        public SearchStatisticsDto? Statistics { get; set; }
    }
} 
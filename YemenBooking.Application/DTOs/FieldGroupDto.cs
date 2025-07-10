namespace YemenBooking.Application.DTOs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// DTO لمجموعة حقول نوع الوحدة
    /// DTO for grouping unit type fields
    /// </summary>
    public class FieldGroupDto
    {
        /// <summary>
        /// معرف المجموعة
        /// Group identifier
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// اسم المجموعة
        /// Group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// الاسم المعروض للمجموعة
        /// Display name of the group
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// وصف المجموعة
        /// Description of the group
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// حقول المجموعة
        /// Fields within the group
        /// </summary>
        public List<UnitTypeFieldDto> Fields { get; set; } = new List<UnitTypeFieldDto>();
    }
} 
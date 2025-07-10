using System;

namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لسجلات التدقيق
    /// DTO for audit log entries
    /// </summary>
    public class AuditLogDto
    {
        /// <summary>
        /// المعرف الفريد للسجل
        /// Log identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// اسم الجدول أو الكيان
        /// Name of the table or entity
        /// </summary>
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// العملية (إنشاء، تحديث، حذف)
        /// Action (Create, Update, Delete)
        /// </summary>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// معرف السجل المتأثر
        /// ID of the affected record
        /// </summary>
        public Guid RecordId { get; set; }

        /// <summary>
        /// معرف المستخدم الذي قام بالتغيير
        /// User ID who performed the change
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// وصف التغييرات
        /// Description of the changes
        /// </summary>
        public string Changes { get; set; } = string.Empty;

        /// <summary>
        /// تاريخ العملية
        /// Timestamp of the action
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
} 
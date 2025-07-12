namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لتفاصيل الوحدة
    /// DTO for unit details
    /// </summary>
    public class UnitDetailsDto : UnitDto
    {
        /// <summary>
        /// السعة القصوى للوحدة (عدد الضيوف الأقصى)
        /// Maximum capacity of the unit (max number of guests)
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// عدد مرات المشاهدة
        /// View count
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// عدد الحجوزات
        /// Booking count
        /// </summary>
        public int BookingCount { get; set; }

        /// <summary>
        /// الحقول الديناميكية مع قيمها
        /// Dynamic field groups with values
        /// </summary>
        public List<FieldGroupWithValuesDto> DynamicFields { get; set; }
    }
} 
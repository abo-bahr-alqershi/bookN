namespace YemenBooking.Application.DTOs
{
    /// <summary>
    /// DTO لتفاصيل الوحدة
    /// DTO for unit details
    /// </summary>
    public class UnitDetailsDto : UnitDto
    {
        /// <summary>
        /// الحقول الديناميكية مع قيمها
        /// Dynamic field groups with values
        /// </summary>
        public List<FieldGroupWithValuesDto> DynamicFields { get; set; }
    }
} 
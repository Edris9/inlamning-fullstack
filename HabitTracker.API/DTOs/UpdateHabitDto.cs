namespace HabitTracker.API.DTOs
{
    public class UpdateHabitDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int FrequencyPerWeek { get; set; }
        public bool IsActive { get; set; }
    }
}
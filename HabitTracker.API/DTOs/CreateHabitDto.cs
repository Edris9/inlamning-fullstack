namespace HabitTracker.API.DTOs
{
    public class CreateHabitDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int FrequencyPerWeek { get; set; }
    }
}   
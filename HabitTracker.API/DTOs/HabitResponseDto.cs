namespace HabitTracker.API.DTOs
{
    public class HabitResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int FrequencyPerWeek { get; set; }
        public int CurrentStreak { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastCompletedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
namespace HabitTracker.API.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int FrequencyPerWeek { get; set; }
        public int CurrentStreak { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastCompletedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
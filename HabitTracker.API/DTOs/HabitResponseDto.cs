using System.ComponentModel.DataAnnotations;

namespace HabitTracker.API.DTOs
{
    public class HabitResponseDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Range(1, 7)]
        public int FrequencyPerWeek { get; set; }
        public int CurrentStreak { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastCompletedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
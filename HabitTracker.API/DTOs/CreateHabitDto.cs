using System.ComponentModel.DataAnnotations;

namespace HabitTracker.API.DTOs
{
    public class CreateHabitDto
    {
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Namn måste vara mellan 3 och 100 tecken")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s]+$", ErrorMessage = "Namn får bara innehålla bokstäver")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Beskrivning måste vara mellan 3 och 500 tecken")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s]+$", ErrorMessage = "Beskrivning får bara innehålla bokstäver")]
        public string Description { get; set; } = string.Empty;

        [Range(1, 7, ErrorMessage = "Frekvens måste vara mellan 1 och 7")]
        public int FrequencyPerWeek { get; set; }
    }
}
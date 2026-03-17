using HabitTracker.API.Data;
using HabitTracker.API.DTOs;
using HabitTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.API.Services
{
    public class HabitService : IHabitService
    {
        private readonly AppDbContext _context;

        public HabitService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HabitResponseDto>> GetAllAsync()
        {
            return await _context.Habits
                .Select(h => ToDto(h))
                .ToListAsync();
        }

        public async Task<HabitResponseDto?> GetByIdAsync(int id)
        {
            var habit = await _context.Habits.FindAsync(id);
            return habit == null ? null : ToDto(habit);
        }

        public async Task<HabitResponseDto> CreateAsync(CreateHabitDto dto)
        {
            var habit = new Habit
            {
                Name = dto.Name,
                Description = dto.Description,
                FrequencyPerWeek = dto.FrequencyPerWeek
            };
            _context.Habits.Add(habit);
            await _context.SaveChangesAsync();
            return ToDto(habit);
        }

        public async Task<HabitResponseDto?> UpdateAsync(int id, UpdateHabitDto dto)
        {
            var habit = await _context.Habits.FindAsync(id);
            if (habit == null) return null;

            habit.Name = dto.Name;
            habit.Description = dto.Description;
            habit.FrequencyPerWeek = dto.FrequencyPerWeek;
            habit.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return ToDto(habit);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var habit = await _context.Habits.FindAsync(id);
            if (habit == null) return false;

            _context.Habits.Remove(habit);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<HabitResponseDto?> CompleteAsync(int id)
        {
            var habit = await _context.Habits.FindAsync(id);
            if (habit == null) return null;

            habit.LastCompletedAt = DateTime.UtcNow;
            habit.CurrentStreak++;

            await _context.SaveChangesAsync();
            return ToDto(habit);
        }

        private static HabitResponseDto ToDto(Habit h) => new HabitResponseDto
        {
            Id = h.Id,
            Name = h.Name,
            Description = h.Description,
            FrequencyPerWeek = h.FrequencyPerWeek,
            CurrentStreak = h.CurrentStreak,
            CreatedAt = h.CreatedAt,
            LastCompletedAt = h.LastCompletedAt,
            IsActive = h.IsActive
        };
    }
}
using HabitTracker.API.DTOs;

namespace HabitTracker.API.Services
{
    public interface IHabitService
    {
        Task<IEnumerable<HabitResponseDto>> GetAllAsync();
        Task<HabitResponseDto?> GetByIdAsync(int id);
        Task<HabitResponseDto> CreateAsync(CreateHabitDto dto);
        Task<HabitResponseDto?> UpdateAsync(int id, UpdateHabitDto dto);
        Task<bool> DeleteAsync(int id);
        Task<HabitResponseDto?> CompleteAsync(int id);
    }
}
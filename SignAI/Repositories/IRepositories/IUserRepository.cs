using SignAI.DTOs;
using SignAI.Models;

namespace SignAI.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<OperationResult<long>> CreatePersonAsync(Person p);
        Task<OperationResult<long>> CreateUserAsync(User u);
        Task<OperationResult<dynamic>> GetUserByEmailAsync(string email);
        Task<OperationResult<long>> CreateMeetingAsync(string title, long hostId, DateTime time, int duration);
        Task<OperationResult> AddParticipantAsync(long meetingId, long userId, string role);

    }
}

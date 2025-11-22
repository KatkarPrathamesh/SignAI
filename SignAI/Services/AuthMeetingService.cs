using SignAI.DTOs;
using SignAI.Models;
using SignAI.DTOs;
using SignAI.Repositories.IRepositories;

namespace SignAI.Services
{
    public class AuthMeetingService
    {
        private readonly IUserRepository _repo;
        public AuthMeetingService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<(long personId, long userId)>> Register(RegisterRequest req)
        {
            var person = new Person { Salutation = req.Salutation, FullNameIntLang = req.FullNameIntLang, EmailId = req.Email, MobileNumber = req.MobileNumber };
            var personResult = await _repo.CreatePersonAsync(person);
            if (!personResult.Success) return OperationResult<(long, long)>.Fail(personResult.Message);

            var userResult = await _repo.CreateUserAsync(new User { PersonId = personResult.Data });
            if (!userResult.Success) return OperationResult<(long, long)>.Fail(userResult.Message);

            return OperationResult<(long, long)>.Ok((personResult.Data, userResult.Data));
        }

        public async Task<OperationResult<dynamic>> Login(string email)
        {
            return await _repo.GetUserByEmailAsync(email);
        }

        public async Task<OperationResult<long>> CreateMeeting(CreateMeetingRequest req)
        {
            var meetingResult = await _repo.CreateMeetingAsync(req.Title, req.UserId, req.ScheduledTime, req.DurationMinutes);
            if (!meetingResult.Success) return OperationResult<long>.Fail(meetingResult.Message);

            var participantResult = await _repo.AddParticipantAsync(meetingResult.Data, req.UserId, "host");
            if (!participantResult.Success) return OperationResult<long>.Fail(participantResult.Message);

            return OperationResult<long>.Ok(meetingResult.Data);
        }

        public async Task<OperationResult> JoinMeeting(JoinMeetingRequest req)
        {
            return await _repo.AddParticipantAsync(req.MeetingId, req.UserId, "guest");
        }
    }

}

using Dapper;
using SignAI.DTOs;
using SignAI.Models;
using SignAI.Services;

namespace SignAI.Repositories.IRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _db;
        public UserRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<OperationResult<long>> CreatePersonAsync(Person p)
        {
            try
            {
                using var conn = await _db.CreateConnectionAsync();

                var exists = await conn.QueryFirstOrDefaultAsync("SELECT 1 FROM Person WHERE emailId = @Email", new { Email = p.EmailId });
                if (exists != null)
                    return OperationResult<long>.Fail("Email already registered", "409");

                var id = await conn.ExecuteScalarAsync<long>(@"
                INSERT INTO Person (salutation, fullNameIntLang, emailId, mobileNumber, createdAt)
                VALUES (@Salutation, @FullNameIntLang, @EmailId, @MobileNumber, NOW());
                SELECT LAST_INSERT_ID();", p);

                return OperationResult<long>.Ok(id, "Person created successfully");
            }
            catch (Exception ex)
            {
                return OperationResult<long>.Fail($"Database error: {ex.Message}");
            }
        }

        public async Task<OperationResult<long>> CreateUserAsync(User u)
        {
            try
            {
                using var conn = await _db.CreateConnectionAsync();
                var id = await conn.ExecuteScalarAsync<long>(@"
                INSERT INTO Users (personId, emailValidationStatus, mobileValidationStatus, isLocked, isMultiSessionAllowed, createdAt)
                VALUES (@PersonId, 0, 0, 0, 1, NOW());
                SELECT LAST_INSERT_ID();", u);

                return OperationResult<long>.Ok(id, "User created successfully");
            }
            catch (Exception ex)
            {
                return OperationResult<long>.Fail($"Database error: {ex.Message}");
            }
        }

        public async Task<OperationResult<dynamic>> GetUserByEmailAsync(string email)
        {
            try
            {
                using var conn = await _db.CreateConnectionAsync();
                var user = await conn.QueryFirstOrDefaultAsync<dynamic>(@"
                SELECT u.*, p.* FROM Users u
                JOIN Person p ON p.id = u.personId
                WHERE p.emailId = @Email", new { Email = email });

                if (user == null)
                    return OperationResult<dynamic>.Fail("User not found", "404");

                return OperationResult<dynamic>.Ok(user);
            }
            catch (Exception ex)
            {
                return OperationResult<dynamic>.Fail($"Database error: {ex.Message}");
            }
        }

        public async Task<OperationResult<long>> CreateMeetingAsync(string title, long hostId, DateTime time, int duration)
        {
            try
            {
                using var conn = await _db.CreateConnectionAsync();
                var id = await conn.ExecuteScalarAsync<long>(@"
                INSERT INTO Meetings (title, hostId, scheduledTime, durationMinutes, status, createdAt)
                VALUES (@Title, @HostId, @Time, @Duration, 'scheduled', NOW());
                SELECT LAST_INSERT_ID();", new { Title = title, HostId = hostId, Time = time, Duration = duration });

                return OperationResult<long>.Ok(id, "Meeting created successfully");
            }
            catch (Exception ex)
            {
                return OperationResult<long>.Fail($"Database error: {ex.Message}");
            }
        }

        public async Task<OperationResult> AddParticipantAsync(long meetingId, long userId, string role)
        {
            try
            {
                using var conn = await _db.CreateConnectionAsync();
                await conn.ExecuteAsync(@"
                INSERT INTO MeetingParticipants (meetingId, userId, role, joinedAt)
                VALUES (@Mid, @Uid, @Role, NOW());", new { Mid = meetingId, Uid = userId, Role = role });

                return OperationResult.Ok("Participant added successfully");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Database error: {ex.Message}");
            }
        }
    }
}

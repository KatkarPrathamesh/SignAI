//using Dapper;
//using SignAI.DTOs;
//using SignAI.Services; 
//using SignAI.Services;
//using System.Data.Common;

//namespace SignAiBackend.Repositories
//{
//    public class MeetingRepository
//    {
//        private readonly DbConnectionFactory _db;

//        public MeetingRepository(DbConnectionFactory db)
//        {
//            _db = db;
//        }

//        public async Task<OperationResult<long>> CreateMeetingAsync(string title, long hostId, DateTime time, int duration)
//        {
//            var sql = @"
//                INSERT INTO Meetings (title, hostId, scheduledTime, durationMinutes, status, createdAt)
//                VALUES (@Title, @HostId, @Time, @Duration, 'scheduled', NOW());
//                SELECT LAST_INSERT_ID();";

//            try
//            {
//                await using var conn = await _db.CreateConnectionAsync();
//                var result = await conn.ExecuteScalarAsync<object>(sql,
//                    new { Title = title, HostId = hostId, Time = time, Duration = duration });

//                return OperationResult<long>.Ok(Convert.ToInt64(result));
//            }
//            catch (Exception ex)
//            {
//                return OperationResult<long>.Fail($"Database error: {ex.Message}");
//            }
//        }

//        public async Task<OperationResult> AddParticipantAsync(long meetingId, long userId, string role)
//        {
//            var sql = @"
//                INSERT INTO MeetingParticipants (meetingId, userId, role, joinedAt)
//                VALUES (@Mid, @Uid, @Role, NOW())";

//            try
//            {
//                await using var conn = await _db.CreateConnectionAsync();
//                await conn.ExecuteAsync(sql, new { Mid = meetingId, Uid = userId, Role = role });
//                return OperationResult.Ok();
//            }
//            catch (Exception ex)
//            {
//                return OperationResult.Fail($"Database error: {ex.Message}");
//            }
//        }
//    }
//}

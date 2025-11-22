using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignAI.DTOs;
using SignAI.Services;

namespace SignAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly AuthMeetingService _service;
        public MainController(AuthMeetingService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            var result = await _service.Register(req);
            return result.Success ? Ok(new { message = "Registered successfully", result.Data.personId, result.Data.userId })
                                  : BadRequest(new { message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var result = await _service.Login(req.Email);
            return result.Success ? Ok(new { user = result.Data }) : BadRequest(new { message = result.Message });
        }

        [HttpPost("create-meeting")]
        public async Task<IActionResult> CreateMeeting(CreateMeetingRequest req)
        {
            var result = await _service.CreateMeeting(req);
            return result.Success ? Ok(new { meetingId = result.Data }) : BadRequest(new { message = result.Message });
        }

        [HttpPost("join-meeting")]
        public async Task<IActionResult> JoinMeeting(JoinMeetingRequest req)
        {
            var result = await _service.JoinMeeting(req);
            return result.Success ? Ok(new { status = "joined" }) : BadRequest(new { message = result.Message });
        }
    }

}

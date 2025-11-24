using Microsoft.AspNetCore.Mvc;
using SignAI.DTOs;
using SignAI.Services;

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

        if (result.Success)
        {
            return Ok(new BaseResponseStatus
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                StatusMessage = "Registered successfully",
                ResponseData = new { result.Data.personId, result.Data.userId }
            });
        }

        return BadRequest(new BaseResponseStatus
        {
            StatusCode = result.StatusCode,
            StatusMessage = result.Message
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var result = await _service.Login(req.Email);

        if (result.Success)
        {
            return Ok(new BaseResponseStatus
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                StatusMessage = "Login successful",
                ResponseData = result.Data
            });
        }

        return BadRequest(new BaseResponseStatus
        {
            StatusCode = result.StatusCode,
            StatusMessage = result.Message
        });
    }

    [HttpPost("create-meeting")]
    public async Task<IActionResult> CreateMeeting(CreateMeetingRequest req)
    {
        var result = await _service.CreateMeeting(req);

        if (result.Success)
        {
            return Ok(new BaseResponseStatus
            {
                StatusCode = "200",
                StatusMessage = "Meeting created successfully",
                ResponseData = result.Data
            });
        }

        return BadRequest(new BaseResponseStatus
        {
            StatusCode = result.StatusCode,
            StatusMessage = result.Message
        });
    }

    [HttpPost("join-meeting")]
    public async Task<IActionResult> JoinMeeting(JoinMeetingRequest req)
    {
        var result = await _service.JoinMeeting(req);

        if (result.Success)
        {
            return Ok(new BaseResponseStatus
            {
                StatusCode = "200",
                StatusMessage = "Joined successfully",
                ResponseData = new { req.MeetingId, req.UserId }
            });
        }

        return BadRequest(new BaseResponseStatus
        {
            StatusCode = result.StatusCode,
            StatusMessage = result.Message
        });
    }
}

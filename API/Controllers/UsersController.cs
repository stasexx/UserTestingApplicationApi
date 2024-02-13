using System.Security.Claims;
using Application.IServices;
using Application.Models.Dtos;
using Application.Models.Identity;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("users")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<TokensModel>> LoginAsync([FromBody] UserLoginDto login, CancellationToken cancellationToken)
    {
        var tokens = await _userService.LoginAsync(login, cancellationToken);
        return Ok(tokens);
    }
    
    [HttpGet("get")]
    public async Task<ActionResult<UserDto>> GetUserAsync(CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(User.FindFirstValue(ClaimTypes.Name), cancellationToken);
        return Ok(user);
    }
}
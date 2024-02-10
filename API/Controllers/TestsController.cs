using Application.IServices;
using Application.Models.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("tests")]
public class TestsController : BaseController
{
    private readonly ITestService _testService;

    public TestsController(ITestService testService)
    {
        _testService = testService;
    }
    
    [Authorize]
    [HttpGet("list/{id}")]
    public async Task<IActionResult> TestsListAsync(Guid id, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _testService.GetTestsPages(id, pageNumber, pageSize, cancellationToken);
        return Ok(result);
    }
    
}
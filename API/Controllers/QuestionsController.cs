using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("questions")]
public class QuestionsController : BaseController
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    
    /*[Authorize]*/
    [HttpGet("list")]
    public async Task<IActionResult> QuestionsListAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _questionService.GetQuestionsPages(pageNumber, pageSize, cancellationToken);
        return Ok(result);
    }
    
    /*[Authorize]*/
    [HttpGet("list/{testId}")]
    public async Task<IActionResult> QuestionsListByTestIdAsync(Guid testId, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _questionService.GetQuestionsByTestIdPages(testId, pageNumber, pageSize, cancellationToken);
        return Ok(result);
    }
}
namespace Application.Models.Dtos;

public class OptionDto
{
    public Guid Id { get; set; }
    
    public string Text { get; set; }
    
    public bool IsCorrect { get; set; }
    
    public Guid QuestionId { get; set; }
}
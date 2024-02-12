namespace Application.Models.Dtos;

public class QuestionOptionDto
{
    public QuestionDto QuestionDto { get; set; }
    
    public OptionDto[] OptionDtos { get; set; }
}
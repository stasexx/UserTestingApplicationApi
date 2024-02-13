using Domain.Entities;

namespace Application.Models.Dtos;

public class QuestionDto
{
    public Guid Id { get; set; } 
    
    public string Title { get; set; }
    
    public double Value { get; set; }
    
    public Guid TestId { get; set; } 
}
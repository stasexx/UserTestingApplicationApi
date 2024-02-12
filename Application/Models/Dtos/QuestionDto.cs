using Domain.Entities;

namespace Application.Models.Dtos;

public class QuestionDto
{
    public Guid Id { get; set; } 
    
    public string Tittle { get; set; }
    
    public string Value { get; set; }
    
    public Guid TestId { get; set; } 
}
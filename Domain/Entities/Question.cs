namespace Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    
    public string Tittle { get; set; }
    
    public string[] Options { get; set; }
    
    public string Answer { get; set; }
    
    public Test Test { get; set; }
    
    public Guid TestId { get; set; }
}
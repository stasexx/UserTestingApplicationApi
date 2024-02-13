namespace Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public List<Option> Options { get; set; }
    
    public double Value { get; set; }
    
    public Test Test { get; set; }
    
    public Guid TestId { get; set; }
}
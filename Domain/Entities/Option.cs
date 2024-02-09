namespace Domain.Entities;

public class Option
{
    public Guid Id { get; set; }
    
    public string Text { get; set; }
    
    public int QuestionId { get; set; }
    
    public Question Question { get; set; }
}
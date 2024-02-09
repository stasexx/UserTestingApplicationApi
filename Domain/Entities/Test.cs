namespace Domain.Entities;

public class Test
{
    public Guid Id { get; set; }
    
    public string Tittle { get; set; }
    
    public List<Question> Questions { get; set; }
}
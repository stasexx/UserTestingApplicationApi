namespace Domain.Entities;

public class Test
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public List<Question> Questions { get; set; }
}
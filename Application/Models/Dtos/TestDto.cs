namespace Application.Models.Dtos;

public class TestDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public double Score { get; set; }
}
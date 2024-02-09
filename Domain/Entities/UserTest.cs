namespace Domain.Entities;

public class UserTest
{
    public bool IsCompleted { get; set; }
    
    public int? Score { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public Guid TestId { get; set; }
    
    public Test Test { get; set; }
    
}
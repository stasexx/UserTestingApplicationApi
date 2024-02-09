namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<UserTest> UserTests { get; set; }
}
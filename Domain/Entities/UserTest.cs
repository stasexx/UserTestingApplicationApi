﻿namespace Domain.Entities;

public class UserTest
{
    public Guid Id { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public double Score { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public Guid TestId { get; set; }
    
    public Test Test { get; set; }
    
}
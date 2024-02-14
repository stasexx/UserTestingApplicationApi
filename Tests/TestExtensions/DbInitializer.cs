using Domain.Entities;
using Persistence.Database;

namespace Tests.TestExtensions;

public class DbInitializer
{
    public static void Initialize(DataContext context)
    {
        context.Database.EnsureCreated();
        
        var users = new List<User>
        {
            new User { Id = Guid.Parse("A7F19456-DC6A-41E1-AC6B-AB5200AAAD61"), Name = "User1"},
            new User { Id = Guid.Parse("A9F19451-DC6A-41E2-AC6B-AB5200AAAD93"), Name = "User2"}
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        var tests = new List<Test>
        {
            new Test { Title = "Test 1", Id = Guid.Parse("A2F19421-DC6A-41E1-AC6B-AB5200AAAD41") },
            new Test { Title = "Test 2", Id = Guid.Parse("A6F19493-DC6A-41E2-AC6B-AB5200AAAD15") }
        };
        context.Tests.AddRange(tests);
        context.SaveChanges();
        
        var questions = new List<Question>
        {
            new Question { TestId = tests[0].Id, Title = "Question 1 for Test 1" },
            new Question { TestId = tests[1].Id, Title = "Question 1 for Test 2" }
        };
        context.Questions.AddRange(questions);
        context.SaveChanges();
        
        var options = new List<Option>
        {
            new Option { QuestionId = questions[0].Id, Text = "Option 1", IsCorrect = true },
            new Option { QuestionId = questions[0].Id, Text = "Option 2", IsCorrect = false },
            new Option { QuestionId = questions[1].Id, Text = "Option 1", IsCorrect = true },
            new Option { QuestionId = questions[1].Id, Text = "Option 2", IsCorrect = false }
        };
        context.Option.AddRange(options);
        context.SaveChanges();
        
        var userTests = new List<UserTest>
        {
            new UserTest { UserId = users[0].Id, TestId = tests[0].Id, IsCompleted = false, Score = 0 },
            new UserTest { UserId = users[0].Id, TestId = tests[1].Id, IsCompleted = false, Score = 0 }
        };
        
        context.UserTests.AddRange(userTests);
        context.SaveChanges();
    }
}
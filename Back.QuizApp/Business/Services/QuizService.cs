using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class QuizService : IQuizService {

    private List<Quiz> _quizzes = new List<Quiz> {
        new Quiz {
            Id = new Guid("ca655c54-0d8b-4ff1-9363-46f269bc5f71"),
            Name = "Dev FullStack M1",
            CategoryId = new Guid("83d82d07-1298-4a4b-9383-00eb985784b0"),
            Description = "ASP.NET Core Web API",
            Difficulty = 3,
            CreationDate = new DateTime(2024, 11, 25, 11, 23, 34),
            Questions = new List<Question> { },
        }, new Quiz {
            Id = new Guid("ca655c54-0d8b-4ff1-9363-46f269bc5f72"),
            Name = "Dev FullStack M2",
            CategoryId = new Guid("83d82d07-1298-4a4b-9383-00eb985744b0"),
            Description = "ASP.NET Core Web API",
            Difficulty = 3,
            CreationDate = new DateTime(2024, 11, 25, 11, 23, 34),
            Questions = new List<Question> { },
        }
    };

    public IEnumerable<Quiz> GetAllQuizzes() {
        return _quizzes;
    }

    public Quiz? GetQuizById(Guid id) {
        return _quizzes.FirstOrDefault(q => q.Id == id);
    }

}
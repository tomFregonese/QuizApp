using System.Text.Json;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers;

public class UserQuizProgressService : IUserQuizProgressService {

    private readonly string _usersQuizProgressFilePath = "DataSet/users-quiz-progress-table.json";
    private readonly string _quizzesFilePath = "DataSet/quizzes-table.json";
    
    private List<UserQuizProgress> usersQuizProgresses;
    private UserQuizProgress userQuizProgress;
    private List<Quiz> quizzes;
    private Quiz quiz;

    public Boolean IsQuizCompleted(Guid userId, Guid quizId) {
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return false;
        }
        userQuizProgress = usersQuizProgresses.FirstOrDefault(uqp => uqp.UserId == userId && uqp.QuizId == quizId);
        
        if (userQuizProgress == null) {
            return false;
        }
        
        quizzes = JsonSerializer.Deserialize<List<Quiz>>(File.ReadAllText(_quizzesFilePath));
        quiz = quizzes.FirstOrDefault(q => q.Id == quizId);
        
        if (userQuizProgress.GivenAnswers.Count < quiz.QuestionIds.Count) {
            return false;
        }
        
        return true;
    }
    
    public Boolean IsQuizStarted(Guid userId, Guid quizId) {
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return false;
        }
        userQuizProgress = usersQuizProgresses.FirstOrDefault(uqp => uqp.UserId == userId && uqp.QuizId == quizId);
        
        if (userQuizProgress == null) {
            return false;
        }
        
        return true;
    }
    
    public Boolean StartAQuiz(Guid userId, Guid quizId) {
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            usersQuizProgresses = new List<UserQuizProgress>();
        }

        userQuizProgress = new UserQuizProgress() {
            UserId = userId,
            QuizId = quizId,
            StartedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = QuizStatus.Started,
            GivenAnswers = new List<List<int>>()
        }; 
        
        usersQuizProgresses.Add(userQuizProgress);
        
        File.WriteAllText(_usersQuizProgressFilePath, JsonSerializer.Serialize(usersQuizProgresses));
        
        return true;
    }
    
    public QuestionIndexAndId GetCurrentQuestion(Guid userId, Guid quizId) {
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return new QuestionIndexAndId();
        }
        userQuizProgress = usersQuizProgresses.FirstOrDefault(uqp => uqp.UserId == userId && uqp.QuizId == quizId);
        
        if (userQuizProgress == null) {
            return new QuestionIndexAndId();
        }
        
        quizzes = JsonSerializer.Deserialize<List<Quiz>>(File.ReadAllText(_quizzesFilePath));
        quiz = quizzes.FirstOrDefault(q => q.Id == quizId);
        
        if (userQuizProgress.GivenAnswers.Count >= quiz.QuestionIds.Count) {
            return new QuestionIndexAndId();
        }
        
        return new QuestionIndexAndId() {
            QuestionId = quiz.QuestionIds[userQuizProgress.GivenAnswers.Count],
            TotalNumberOfQuestions = quiz.QuestionIds.Count,
            CurrentQuestionIndex = userQuizProgress.GivenAnswers.Count
        };
    }
    
    public Boolean AnswerQuestion(Guid userId, Guid questionId, List<int> selectedOptions) {
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return false;
        }

        UserQuizProgress mostRecentUserQuizProgress = null; 
        int mostRecentUserQuizProgressIndex = -1;
        
        for (int i =0; i< usersQuizProgresses.Count; i++) {
            if (mostRecentUserQuizProgress == null || (usersQuizProgresses[i].UserId == userId && usersQuizProgresses[i].QuizId == 
                questionId && usersQuizProgresses[i].StartedAt > mostRecentUserQuizProgress.StartedAt)) {
                mostRecentUserQuizProgress = usersQuizProgresses[i];
                mostRecentUserQuizProgressIndex = i;
            }
        }
        
        if (mostRecentUserQuizProgress == null) {
            return false;
        }
        
        Boolean isItThisQuestionTheUserHasToAnswerTo = GetCurrentQuestion(userId, mostRecentUserQuizProgress.QuizId).QuestionId == questionId;
        
        if (!isItThisQuestionTheUserHasToAnswerTo) {
            return false;
        }
        
        mostRecentUserQuizProgress.GivenAnswers.Add(selectedOptions);
        mostRecentUserQuizProgress.UpdatedAt = DateTime.Now;
        usersQuizProgresses[mostRecentUserQuizProgressIndex] = mostRecentUserQuizProgress; 
        File.WriteAllText(_usersQuizProgressFilePath, JsonSerializer.Serialize(usersQuizProgresses));

        return true;
    }

}
using System.Text.Json;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers;

public class UserQuizProgressService : IUserQuizProgressService {

    private readonly string _usersQuizProgressFilePath = "DataSet/users-quiz-progress-table.json";
    private readonly string _quizzesFilePath = "DataSet/quizzes-table.json";
    private readonly string _questionsFilePath = "DataSet/questions-table.json";
    private readonly IQuestionService _questionService;
    private readonly IQuizService _quizService;
    
    public UserQuizProgressService(IQuestionService questionService, IQuizService quizService) {
        _questionService = questionService;
        _quizService = quizService; 
    }
    
    private List<UserQuizProgress>? _usersQuizProgresses;
    private UserQuizProgress? _userQuizProgress;
    private List<Quiz> quizzes;
    private Quiz quiz;

    public List<UserQuizProgress>? LoadUsersQuizProgresses() {
        var usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText
            (_usersQuizProgressFilePath));
        return usersQuizProgresses?.Count > 0 ? usersQuizProgresses : null;
    }
    
    public UserQuizProgress? GetCurrentUQP(Guid userId, Guid quizId) {
        _usersQuizProgresses = LoadUsersQuizProgresses();
        if (_usersQuizProgresses?.Count == 0 || _usersQuizProgresses == null) {
            return null;
        }
        return _usersQuizProgresses.Where(uqp => uqp.UserId == userId && uqp.QuizId == quizId && uqp.Status == QuizStatus.Started).OrderByDescending
            (uqp => uqp.StartedAt).FirstOrDefault();
    }
    
    public UserQuizProgress? GetMostRecentUQP(Guid userId, Guid quizId) { 
        _usersQuizProgresses = LoadUsersQuizProgresses();
        if (_usersQuizProgresses == null) {
            return null;
        }
        return _usersQuizProgresses.Where(uqp => uqp.UserId == userId && uqp.QuizId == quizId).OrderByDescending(uqp => uqp.StartedAt).FirstOrDefault();
    }

    public Boolean IsQuizCompleted(Guid userId, Guid quizId) {
        UserQuizProgress? mostRecentUserQuizProgress = GetMostRecentUQP(userId, quizId);
        if (mostRecentUserQuizProgress == null) {
            return false;
        }

        return mostRecentUserQuizProgress.Status != QuizStatus.Started;
    }
    
    public Boolean IsQuizStarted(Guid userId, Guid quizId) {
        UserQuizProgress? mostRecentUserQuizProgress = GetMostRecentUQP(userId, quizId);
        return mostRecentUserQuizProgress?.Status == QuizStatus.Started;
    }
    
    public Boolean StartAQuiz(Guid userId, Guid quizId) {
        _usersQuizProgresses = LoadUsersQuizProgresses();
        _usersQuizProgresses ??= new List<UserQuizProgress>();

        _userQuizProgress = new UserQuizProgress() {
            UserId = userId,
            QuizId = quizId,
            StartedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = QuizStatus.Started,
            GivenAnswers = new List<List<int>>()
        }; 
        
        _usersQuizProgresses.Add(_userQuizProgress);
        
        File.WriteAllText(_usersQuizProgressFilePath, JsonSerializer.Serialize(_usersQuizProgresses));
        
        return true;
    }
    
    public QuestionIndexAndId GetCurrentQuestion(Guid userId, Guid quizId) {
        _userQuizProgress = GetCurrentUQP(userId, quizId);
        if (_userQuizProgress == null) {
            return new QuestionIndexAndId();
        }
        
        quiz = _quizService.GetQuizById(quizId);
        
        if (_userQuizProgress.GivenAnswers.Count >= quiz.QuestionIds.Count) {
            return new QuestionIndexAndId();
        }
        
        return new QuestionIndexAndId() {
            QuestionId = quiz.QuestionIds[_userQuizProgress.GivenAnswers.Count],
            TotalNumberOfQuestions = quiz.QuestionIds.Count,
            CurrentQuestionIndex = _userQuizProgress.GivenAnswers.Count
        };
    }
    
    public Boolean IsItTheLastQuestion(UserQuizProgress uqp) {
        quiz = _quizService.GetQuizById(uqp.QuizId);
        return uqp.GivenAnswers.Count == quiz.QuestionIds.Count - 1;
    }
    
    public Boolean AnswerQuestion(Guid userId, Guid quizId, Guid questionId, List<int> selectedOptions) {
        _userQuizProgress = GetCurrentUQP(userId, quizId);
        if (_userQuizProgress == null) {
            return false;
        }
        
        int currentQuizProgressIndex = _usersQuizProgresses.IndexOf(_userQuizProgress);
        
        Boolean isItThisQuestionTheUserHasToAnswerTo = GetCurrentQuestion(userId, _userQuizProgress.QuizId).QuestionId == questionId;
        if (!isItThisQuestionTheUserHasToAnswerTo) {
            return false;
        }
        
        Boolean isItTheLastQuestion;
        isItTheLastQuestion = IsItTheLastQuestion(_userQuizProgress);
        
        _userQuizProgress.GivenAnswers.Add(selectedOptions);
        _userQuizProgress.UpdatedAt = DateTime.Now;
        _userQuizProgress.Status = isItTheLastQuestion ? QuizStatus.Completed : QuizStatus.Started;
        _usersQuizProgresses[currentQuizProgressIndex] = _userQuizProgress; 
        File.WriteAllText(_usersQuizProgressFilePath, JsonSerializer.Serialize(_usersQuizProgresses));

        return true;
    }
    
    public Answer GetAnswersByQuestionId(Guid userId, Guid quizId, Guid questionId) {
        //Check if the user has answered the question
        _userQuizProgress = GetMostRecentUQP(userId, quizId);
        if (_userQuizProgress == null) {
            return new Answer();
        }
        
        int wantedQuestionIndex = _questionService.GetQuestionIndex(_userQuizProgress.QuizId, questionId);
        if (wantedQuestionIndex > (_userQuizProgress.GivenAnswers.Count -1)) { 
            return new Answer(); 
        }

        //Get the answer of the question
        var questions = _questionService.GetAllQuestions();
        return new Answer { CorrectOptionIndices = questions.FirstOrDefault(q => q.Id == questionId).CorrectOptionIndices };
    }

    public void CloseUnfinishedQuizzes() {
        _usersQuizProgresses = LoadUsersQuizProgresses(); 
        if (_usersQuizProgresses == null) {
            return;
        }

        foreach (var userQuizProgress in _usersQuizProgresses) {
            if (userQuizProgress.Status == QuizStatus.Started && (DateTime.Now - userQuizProgress.UpdatedAt).TotalMinutes > 15) {
                userQuizProgress.Status = QuizStatus.Abandoned;
                Console.WriteLine("Closed unfinished quiz for user: " + userQuizProgress.UserId);
            }
        }
        
        File.WriteAllText(_usersQuizProgressFilePath, JsonSerializer.Serialize(_usersQuizProgresses));
    }

}
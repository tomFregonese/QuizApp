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
    
    private List<UserQuizProgress> usersQuizProgresses;
    private UserQuizProgress userQuizProgress;
    private List<Quiz> quizzes;
    private Quiz quiz;

    public Boolean IsQuizCompleted(Guid userId, Guid quizId) {
        List<UserQuizProgress> usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return false;
        }
        
        UserQuizProgress mostRecentUserQuizProgress = usersQuizProgresses.Where(uqp => uqp.UserId == userId && uqp.QuizId == quizId).OrderByDescending(uqp => uqp.StartedAt).FirstOrDefault();
        if (mostRecentUserQuizProgress == null) {
            return false;
        }

        return mostRecentUserQuizProgress.Status != QuizStatus.Started;
    }
    
    public Boolean IsQuizStarted(Guid userId, Guid quizId) {
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return false;
        }
        
        UserQuizProgress mostRecentUserQuizProgress = usersQuizProgresses.Where(uqp => uqp.UserId == userId && uqp.QuizId == quizId).OrderByDescending(uqp => uqp.StartedAt).FirstOrDefault();
        if (mostRecentUserQuizProgress == null || mostRecentUserQuizProgress.Status != QuizStatus.Started) {
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
    
    public Boolean IsItTheLastQuestion(UserQuizProgress uqp) {
        quiz = _quizService.GetQuizById(uqp.QuizId);
        return uqp.GivenAnswers.Count == quiz.QuestionIds.Count - 1;
    }
    
    public Boolean AnswerQuestion(Guid userId, Guid questionId, List<int> selectedOptions) {
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return false;
        }

        UserQuizProgress currentQuizProgress = usersQuizProgresses.FirstOrDefault(uqp => uqp.UserId == userId && uqp.Status == QuizStatus.Started);
        if (currentQuizProgress == null) {
            return false;
        }
        
        int currentQuizProgressIndex = usersQuizProgresses.IndexOf(currentQuizProgress);
        
        Boolean isItThisQuestionTheUserHasToAnswerTo = GetCurrentQuestion(userId, currentQuizProgress.QuizId).QuestionId == questionId;
        if (!isItThisQuestionTheUserHasToAnswerTo) {
            return false;
        }
        
        Boolean isItTheLastQuestion;
        isItTheLastQuestion = IsItTheLastQuestion(currentQuizProgress);
        
        currentQuizProgress.GivenAnswers.Add(selectedOptions);
        currentQuizProgress.UpdatedAt = DateTime.Now;
        currentQuizProgress.Status = isItTheLastQuestion ? QuizStatus.Completed : QuizStatus.Started;
        usersQuizProgresses[currentQuizProgressIndex] = currentQuizProgress; 
        File.WriteAllText(_usersQuizProgressFilePath, JsonSerializer.Serialize(usersQuizProgresses));

        return true;
    }
    
    public Answer GetAnswersByQuestionId(Guid userId, Guid questionId) {
        //Check if the user has answered the question
        usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return new Answer();
        }
        
        UserQuizProgress mostRecentUserQuizProgress = null; 
        
        for (int i =0; i< usersQuizProgresses.Count; i++) { //Use the UserQuizProgress status to know which one is the most recent
            if (mostRecentUserQuizProgress == null || (usersQuizProgresses[i].UserId == userId && usersQuizProgresses[i].QuizId == 
                    questionId && usersQuizProgresses[i].StartedAt > mostRecentUserQuizProgress.StartedAt)) {
                mostRecentUserQuizProgress = usersQuizProgresses[i];
            }
        }
        
        if (mostRecentUserQuizProgress == null) {
            return new Answer();
        }
        
        int wantedQuestionIndex = _questionService.GetQuestionIndex(mostRecentUserQuizProgress.QuizId, questionId);
        if (wantedQuestionIndex > (mostRecentUserQuizProgress.GivenAnswers.Count -1)) { 
            return new Answer(); 
        }

        //Get the answer of the question
        var jsonData = File.ReadAllText(_questionsFilePath);
        var data = JsonSerializer.Deserialize<List<Question>>(jsonData);
        var questions = data ?? new List<Question>();
        
        return new Answer { CorrectOptionIndices = questions.FirstOrDefault(q => q.Id == questionId).CorrectOptionIndices };
    }

    public void CloseUnfinishedQuizzes() {
        List<UserQuizProgress> usersQuizProgresses = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizProgressFilePath));
        if (usersQuizProgresses == null) {
            return;
        }

        foreach (var userQuizProgress in usersQuizProgresses) {
            if (userQuizProgress.Status == QuizStatus.Started && (DateTime.Now - userQuizProgress.UpdatedAt).TotalMinutes > 15) {
                userQuizProgress.Status = QuizStatus.Abandoned;
                Console.WriteLine("Closed unfinished quiz for user: " + userQuizProgress.UserId);
            }
        }
        
        File.WriteAllText(_usersQuizProgressFilePath, JsonSerializer.Serialize(usersQuizProgresses));
    }

}
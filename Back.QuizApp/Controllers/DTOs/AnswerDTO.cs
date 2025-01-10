namespace Ynov.QuizApp.Controllers.DTOs;

public class AnswerDTO {
    
    public required List<int> CorrectOptionIndices { get; set; }
    
}

public class GivenAnswerDTO {
    
    public required List<int> SelectedOptionIndices { get; set; }
    
}
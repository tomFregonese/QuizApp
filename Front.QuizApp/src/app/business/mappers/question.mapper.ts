import {Injectable} from '@angular/core';
import {QuestionDto} from '../dtos/question.dto';
import {Question} from '../models/question.model';
import {AnswerDto} from '../dtos/answer.dto';

@Injectable({providedIn: 'root'})
export class QuestionMapper {
    public mapQuestionFromApiToModel(apiQuestion: QuestionDto): Question {
        return {
            id: apiQuestion.id,
            quizId: apiQuestion.quizId,
            questionContent: apiQuestion.questionContent,
            options: apiQuestion.options,
            correctOptionIndices: []
        };
    }
}

@Injectable({providedIn: 'root'})
export class AnswerMapper {
    public mapAnswerFromApiToModel(originalQuestion: Question, apiAnswer: AnswerDto): Question {
        originalQuestion.correctOptionIndices = apiAnswer.correctOptionIndices
        return originalQuestion
    }
}

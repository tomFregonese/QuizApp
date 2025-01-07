import {Injectable} from '@angular/core';
import {QuestionIndexAndIdDto} from '../dtos/questionIndexAndId.dto';
import {QuestionIndexAndId} from '../models/questionIndexAndId.model';

@Injectable({providedIn: 'root'})
export class QuestionIndexAndIdMapper {
    public mapFromApiToModel(apiQuestionIndexAndId: QuestionIndexAndIdDto): QuestionIndexAndId {
        return {
            questionId: apiQuestionIndexAndId.questionId,
            totalNumberOfQuestions: apiQuestionIndexAndId.totalNumberOfQuestions,
            currentQuestionIndex: apiQuestionIndexAndId.currentQuestionIndex,
        };
    }

}

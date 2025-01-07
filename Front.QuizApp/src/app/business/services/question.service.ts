import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {environment} from '../../environment/environment';
import {AnswerMapper, QuestionMapper} from '../mappers/question.mapper';
import {Question} from '../models/question.model';
import {QuestionDto} from '../dtos/question.dto';
import {QuestionIndexAndId} from '../models/questionIndexAndId.model';
import {QuestionIndexAndIdDto} from '../dtos/questionIndexAndId.dto';
import {QuestionIndexAndIdMapper} from '../mappers/questionIndexAndId.mapper';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
    private readonly questionApiUrl: string = environment.apiUrl;

    constructor(private readonly questionIndexAndIdMapper: QuestionIndexAndIdMapper,
                private readonly questionMapper: QuestionMapper,
                private readonly answerMapper: AnswerMapper,
                private readonly httpClient: HttpClient) {}

    getCurrentQuestion(userId: string, quizId: string): Observable<QuestionIndexAndId> {
        return this.httpClient.get<QuestionIndexAndIdDto>(this.questionApiUrl + `get-current-question/${userId}/${quizId}`)
            .pipe(
                map((dto: QuestionIndexAndIdDto) => {
                    return this.questionIndexAndIdMapper.mapFromApiToModel(dto)
                })
            );
    }

    getQuestionById(questionId: string):Observable<Question> {
        return this.httpClient.get<QuestionDto>(this.questionApiUrl + 'question/' + questionId)
            .pipe(
                map((dto: QuestionDto) => {
                    return this.questionMapper.mapQuestionFromApiToModel(dto)
                })
            );
    }

    getAnswerByQuestionId(userId: string, questionId: string) {
        //TODO implement this method
    }

}

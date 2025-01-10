import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable, switchMap} from 'rxjs';
import {environment} from '../../environment/environment';
import {AnswerMapper, QuestionMapper} from '../mappers/question.mapper';
import {Question} from '../models/question.model';
import {QuestionDto} from '../dtos/question.dto';
import {QuestionIndexAndId} from '../models/questionIndexAndId.model';
import {QuestionIndexAndIdDto} from '../dtos/questionIndexAndId.dto';
import {QuestionIndexAndIdMapper} from '../mappers/questionIndexAndId.mapper';
import {UserService} from './user.service';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
    private readonly questionApiUrl: string = environment.apiUrl;

    constructor(private readonly questionIndexAndIdMapper: QuestionIndexAndIdMapper,
                private readonly questionMapper: QuestionMapper,
                private readonly answerMapper: AnswerMapper,
                private readonly httpClient: HttpClient,
                private readonly userService: UserService,) {}

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

    postAnswer(questionId: string, selectedOptions: number[]): Observable<Boolean> {
    return this.userService.getUserId().pipe(switchMap(
        userId => this.httpClient.post<Boolean>(this.questionApiUrl + `answer-question/${userId}/${questionId}`, selectedOptions)
        ));
    }

    getAnswerByQuestionId(userId: string, questionId: string) {
        //TODO implement this method
    }

}

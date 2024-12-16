import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {environment} from '../../environment/environment';
import {AnswerMapper, QuestionMapper} from '../mappers/question.mapper';
import {Question} from '../models/question.model';
import {QuestionDto} from '../dtos/question.dto';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
    private readonly quizApiUrl: string = environment.apiUrl;

    constructor(private readonly questionMapper: QuestionMapper,
                private readonly answerMapper: AnswerMapper,
                private readonly httpClient: HttpClient) {}

    getQuestionByQuizId(quizId: string):Observable<Question[]> {
        return this.httpClient.get<QuestionDto[]>(this.quizApiUrl + 'questions/' + quizId)
            .pipe(
                map((dtos: QuestionDto[]) => {
                    return dtos.map((dto: QuestionDto) => this.questionMapper.mapQuestionFromApiToModel(dto));
                }),
            );
    }

    getAnswerByQuestionId(questionId: string) {
        //TODO implement this method
    }

}

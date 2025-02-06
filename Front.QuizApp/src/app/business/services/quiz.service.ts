import { Injectable } from '@angular/core';
import {QuizMapper} from '../mappers/quiz.mapper';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {Quiz} from '../models/quiz.model';
import {QuizDto} from '../dtos/quiz.dto';
import {environment} from '../../environment/environment';
import {QuizStatus} from '../models/userQuizProgressStatus.model';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
    private readonly quizApiUrl: string = environment.apiUrl;

    constructor( private readonly  mapper: QuizMapper,
               private readonly httpClient: HttpClient) {}


    public getAllQuizzes(): Observable<Quiz[]> {
    return this.httpClient.get<QuizDto[]>(this.quizApiUrl + 'all-quizzes')
        .pipe(
            map((dtos: QuizDto[]) => {
              return dtos.map((dto: QuizDto) : Quiz => this.mapper.mapQuizFromApiToModel(dto));
            }),
        );
    }

    public getQuizById(quizId: string): Observable<Quiz> {
    return this.httpClient.get<QuizDto>(this.quizApiUrl + 'quiz/' + quizId)
        .pipe(
            map((dto: QuizDto) => {
                return this.mapper.mapQuizFromApiToModel(dto)
            }),
        );
    }

    getQuizStatus(userId: string, quizId: string): Observable<QuizStatus> {
        return this.httpClient.get<QuizStatus>(this.quizApiUrl + `get-quiz-status/${userId}/${quizId}`)
            .pipe(map((status: QuizStatus) => status));
    }

    startQuiz(userId: string, quizId: string) {
        return this.httpClient.post(this.quizApiUrl + `start-quiz/${userId}/${quizId}`, null);
    }
}

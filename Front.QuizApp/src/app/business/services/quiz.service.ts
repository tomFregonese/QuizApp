import { Injectable } from '@angular/core';
import {QuizMapper} from '../mappers/quiz.mapper';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {Quiz} from '../models/quiz.model';
import {QuizDto} from '../dtos/quiz.dto';
import {environment} from '../../environment/environment';

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

    isQuizStarted(quizId: string): Observable<boolean> {
        return this.httpClient.get<boolean>(this.quizApiUrl + 'is-quiz-started/' + quizId)
            .pipe(
                map((started: boolean) => {
                    return started;
                })
            );
    }

    startQuiz(quizId: string) {
        return this.httpClient.post(this.quizApiUrl + 'start-quiz/' + quizId, null);
    }
}

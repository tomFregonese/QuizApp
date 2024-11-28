import { Injectable } from '@angular/core';
import {QuizMapper} from './mappers/quiz.mapper';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {Quiz} from '../models/quiz.model';
import {QuizDto} from './dtos/quiz.dto';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
    private readonly protocol: string = 'http';
    private readonly host: string = 'localhost';
    private readonly port: string = '5001';
    private readonly apiVersion: string = 'v1';
    private readonly quizApiUrl: string = this.protocol + '://' + this.host + ':' + this.port + '/' +
                                          this.apiVersion + '/';

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

    public getQuizById(id: string): Observable<Quiz> {
    return this.httpClient.get<QuizDto>(this.quizApiUrl + 'quiz/' + id)
        .pipe(
            map((dto: QuizDto) => {
              return this.mapper.mapQuizFromApiToModel(dto)
            }),
        );
    }

    public getCategoryNameById(categoryId: string) {
        return categoryId; //Todo implement this to get the category name from the category id
    }
}

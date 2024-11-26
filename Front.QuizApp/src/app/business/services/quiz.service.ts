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

  private quizApiV1Url: string = 'http://localhost:5001/v1/';

  constructor( private readonly  mapper: QuizMapper,
               private readonly httpClient: HttpClient) {}


  public getAllQuizzes(): Observable<Quiz[]> {
    return this.httpClient.get<QuizDto[]>(this.quizApiV1Url + 'quizzes')
        .pipe(
            map((dtos: QuizDto[]) => {
              return dtos.map((dto: QuizDto) : Quiz => this.mapper.mapQuizFromApiToModel(dto));
            }),
        );
  }

  public getQuizById(id: string): Observable<Quiz> {
    return this.httpClient.get<QuizDto>(this.quizApiV1Url + '/' + id)
        .pipe(
            map((dto: QuizDto) => {
              return this.mapper.mapQuizFromApiToModel(dto)
            }),
        );
  }

}

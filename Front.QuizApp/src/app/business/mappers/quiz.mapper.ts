import {Injectable} from '@angular/core';
import {QuizDto} from '../dtos/quiz.dto';
import {Quiz} from '../models/quiz.model';

@Injectable({providedIn: 'root'})
export class QuizMapper {
    public mapQuizFromApiToModel(apiQuiz: QuizDto): Quiz {
        return {
            id: apiQuiz.id,
            name: apiQuiz.name,
            description: apiQuiz.description,
            difficulty: apiQuiz.difficulty,
            creationDate: new Date(apiQuiz.creationDate),
            categoryId: apiQuiz.categoryId,
        };
    }
}

import {Component, OnInit} from '@angular/core';
import {QuizService} from '../../business/services/quiz.service';
import {Quiz} from '../../business/models/quiz.model';
import {Subscription} from 'rxjs';
import {QuizCardComponent} from '../quiz-card/quiz-card.component';
import {CategoryService} from '../../business/services/category.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    QuizCardComponent
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit{
  constructor (protected readonly quizService: QuizService,
               protected readonly categoryService: CategoryService,) {}

  private subscription?: Subscription;

  public quizzesFilteredByCategory: Map<string, Quiz[]> = new Map<string, Quiz[]>();

  ngOnInit() {
    this.subscription = this.quizService.getAllQuizzes()
        .subscribe((quizzes: Quiz[]) => {
          for (const quiz of quizzes) {
            let quizzesList = this.quizzesFilteredByCategory.get(quiz.categoryId);
            if (!quizzesList) {
              quizzesList = [];
            }
            quizzesList.push(quiz);
            this.quizzesFilteredByCategory.set(quiz.categoryId, quizzesList);
          }
    });
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

}

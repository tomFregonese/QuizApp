import {Component, OnInit} from '@angular/core';
import {QuizService} from '../../../business/services/quiz.service';
import {Quiz} from '../../../business/models/quiz.model';
import {QuizDto} from '../../../business/services/dtos/quiz.dto';
import {RouterLink} from '@angular/router';
import {of} from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit{
  constructor (private readonly quizService: QuizService) {}

  public quizzes: Quiz[] = [];

  ngOnInit() {
    this.quizService.getAllQuizzes().subscribe((quizzes: Quiz[]) => {
      this.quizzes = quizzes;
    });
  }

  ngOnDestroy() {}

  protected readonly of = of;
  protected readonly Array = Array;
}

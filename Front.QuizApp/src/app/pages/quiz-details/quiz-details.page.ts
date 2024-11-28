import {Component, OnInit} from '@angular/core';
import {QuizService} from '../../business/services/quiz.service';
import {Subscription} from 'rxjs';
import {Quiz} from '../../business/models/quiz.model';
import {DatePipe} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-quiz-details',
  standalone: true,
  imports: [
    DatePipe
  ],
  templateUrl: './quiz-details.page.html',
  styleUrl: './quiz-details.page.scss'
})
export class QuizDetailsPage implements OnInit{

  constructor (private readonly quizService: QuizService,
               private router: Router,
               private readonly route: ActivatedRoute, ) {}

  private subscription?: Subscription;

  public quiz!: Quiz;

  ngOnInit() {
    const quizId = this.route.snapshot.paramMap.get('id');
    this.subscription = this.quizService.getQuizById(quizId!).subscribe((quiz: Quiz) => {
      this.quiz = quiz;
    });
  }


  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

  startQuiz() {
    if (this.quiz) {
      this.router.navigate(['/quiz/' + this.quiz.id + '/questions']);
    }
  }
}

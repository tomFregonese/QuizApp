import {Component, Input} from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import {Quiz} from '../../business/models/quiz.model';

@Component({
  selector: 'app-quiz-card',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './quiz-card.component.html',
  styleUrl: './quiz-card.component.scss'
})
export class QuizCardComponent {
    @Input() quiz!: Quiz;
    protected readonly Array = Array;

    constructor (private readonly router: Router) {}

    onQuizClicked(quizId: string) {
        this.router.navigate(['/quiz', quizId]);
    }
}

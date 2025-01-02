import {Component, Input} from '@angular/core';
import {Question} from '../../business/models/question.model';

@Component({
  selector: 'app-quiz-question',
  standalone: true,
  imports: [],
  templateUrl: './quiz-question.component.html',
  styleUrl: './quiz-question.component.scss'
})
export class QuizQuestionComponent {
    @Input() question!: Question;
    constructor() {}

    protected option: string = '';


    selectOption(i: number) {
        // TODO: Implement this method
    }
}

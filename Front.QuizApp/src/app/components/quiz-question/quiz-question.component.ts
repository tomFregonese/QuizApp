import {Component, EventEmitter, Input, Output} from '@angular/core';
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
    @Output() selectedOptionsEmitter: EventEmitter<Set<number>> = new EventEmitter<Set<number>>();
    constructor() {}

    public selectedOptions: Set<number> = new Set<number>();

    toggleOption(optionIndex: number) {
        if (this.selectedOptions.has(optionIndex)) {
            this.selectedOptions.delete(optionIndex);
        } else {
            this.selectedOptions.add(optionIndex);
        }
        this.selectedOptionsEmitter.emit(this.selectedOptions);
    }

    isSelected(optionIndex: number) {
        return this.selectedOptions.has(optionIndex);
    }
}

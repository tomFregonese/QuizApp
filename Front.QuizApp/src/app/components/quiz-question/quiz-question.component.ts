import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Question} from '../../business/models/question.model';
import {NgClass} from '@angular/common';
import {LoadingSpinnerComponent} from '../loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-quiz-question',
  standalone: true,
    imports: [
        NgClass,
        LoadingSpinnerComponent
    ],
  templateUrl: './quiz-question.component.html',
  styleUrl: './quiz-question.component.scss'
})
export class QuizQuestionComponent {
    @Input() question!: Question;
    @Output() selectedOptionsEmitter: EventEmitter<Set<number>> = new EventEmitter<Set<number>>();
    constructor() {}

    public selectedOptions: Set<number> = new Set<number>();
    public optionClasses: string[] = [];
    public loading: boolean = false;

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

    displayCorrectOption() {
        this.optionClasses = this.question.options.map((_, index) => {
            if (this.question.correctOptionIndices.includes(index)) {
                return this.selectedOptions.has(index) ? 'correct-selected' : 'correct-not-selected';
            } else {
                return this.selectedOptions.has(index) ? 'incorrect-selected' : '';
            }
        });
    }

    hideCorrectOption() {
        this.optionClasses = [];
    }

}

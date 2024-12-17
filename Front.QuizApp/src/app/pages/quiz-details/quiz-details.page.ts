import {Component, OnInit} from '@angular/core';
import {QuizService} from '../../business/services/quiz.service';
import {Subscription} from 'rxjs';
import {Quiz} from '../../business/models/quiz.model';
import {DatePipe} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {CategoryService} from '../../business/services/category.service';
import {QuizQuestionComponent} from '../../components/quiz-question/quiz-question.component';
import {Question} from '../../business/models/question.model';
import {QuestionService} from '../../business/services/question.service';
import {UserService} from '../../business/services/user.service';
import {UserHeaderComponent} from '../../components/user-header/user-header.component';

@Component({
    selector: 'app-quiz-details',
    standalone: true,
    imports: [
        DatePipe,
        QuizQuestionComponent,
        UserHeaderComponent
    ],
    templateUrl: './quiz-details.page.html',
    styleUrl: './quiz-details.page.scss'
})

export class QuizDetailsPage implements OnInit{

    constructor (private readonly questionService: QuestionService,
                 private readonly quizService: QuizService,
                 private readonly categoryService: CategoryService,
                 private readonly userService: UserService,
                 private readonly router: Router,
                 private readonly route: ActivatedRoute, ) {}

    private subscriptions: Subscription = new Subscription();
    public quiz!: Quiz;
    protected categoryName: string = '';
    protected quizStarted: boolean = false;
    protected questions: Question[] = [];

    ngOnInit() {
        const quizId = this.route.snapshot.paramMap.get('id');
        this.subscriptions.add(
            this.quizService.isQuizStarted(quizId!).subscribe((started: boolean) => {
                this.quizStarted = started;
            })
        );
        this.subscriptions.add(
            this.quizService.getQuizById(quizId!).subscribe((quiz: Quiz) => {
                this.quiz = quiz;
                this.categoryName = this.categoryService.getCategoryNameById(this.quiz.categoryId);
            })
        );
    }

    ngOnDestroy() {
        this.subscriptions.unsubscribe();
    }

    startQuiz() {
        if (!this.userService.isUserLoggedIn()) {
            this.router.navigate(['/login']);
            return;
        }
        this.subscriptions.add(
            this.quizService.startQuiz(this.quiz.id).subscribe(() => {
                this.quizStarted = true;
            })
        );
        this.subscriptions.add(
            this.questionService.getQuestionByQuizId(this.quiz.id).subscribe((questions: Question[]) => {
                this.questions = questions;
                console.log(this.questions)
            })
        );
    }

    validateQuestion() {
        // Validate the question
    }

}

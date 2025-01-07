import {Component, OnInit} from '@angular/core';
import {QuizService} from '../../business/services/quiz.service';
import {Observable, Subscription} from 'rxjs';
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
    protected currentQuestionIndex: number = 0;
    protected totalNumberOfQuestions: number = 0;
    protected question: Question = {
        correctOptionIndices: [],
        id: '',
        options: [],
        questionContent: '',
    };
    private userId: string = '';

    ngOnInit() {
        const quizId = this.route.snapshot.paramMap.get('id');
        this.subscriptions.add(
            this.userService.getUserId().subscribe((userId: string) =>{
                this.userId = userId;
                this.quizService.isQuizStarted(userId, quizId!).subscribe((started: boolean) => {
                    this.quizStarted = started;
                    if (started) this.getQuestion(this.userId, quizId!);
                })
            } )
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
            this.quizService.startQuiz(this.userId, this.quiz.id).subscribe(() => {
                this.quizStarted = true;
                this.getQuestion(this.userId, this.quiz.id);
            })
        );
    }


    getQuestion(userId: string, quizId: string) {
        this.questionService.getCurrentQuestion(userId, quizId).subscribe((questionIndexAndId) => {
                this.totalNumberOfQuestions = questionIndexAndId.totalNumberOfQuestions;
                this.currentQuestionIndex = questionIndexAndId.currentQuestionIndex;
                this.questionService.getQuestionById(questionIndexAndId.questionId).subscribe((question: Question) => {
                    this.question = question;
                    console.log(this.question)
                });
            }
        )
    }

    validateQuestion() {
        // Validate the question
    }

}

import {Component, OnInit, ViewChild} from '@angular/core';
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
import {QuizStatus} from '../../business/models/userQuizProgressStatus.model';

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
    @ViewChild(QuizQuestionComponent) quizQuestionComponent!: QuizQuestionComponent;

    constructor (private readonly questionService: QuestionService,
                 private readonly quizService: QuizService,
                 private readonly categoryService: CategoryService,
                 private readonly userService: UserService,
                 private readonly router: Router,
                 private readonly route: ActivatedRoute, ) {}

    private subscriptions: Subscription = new Subscription();
    public quiz!: Quiz;
    protected categoryName: string = '';
    protected quizStatus: QuizStatus = QuizStatus.notInitialized;
    protected displayRecap: boolean = false;
    protected currentQuestionIndex: number = 0;
    protected totalNumberOfQuestions: number = 0;
    protected question: Question = {
        correctOptionIndices: [],
        id: '',
        options: [],
        questionContent: '',
    };
    protected selectedOptions: Set<number> = new Set<number>();
    private userId: string = '';
    protected readonly QuizStatus = QuizStatus;

    ngOnInit() {
        const quizId = this.route.snapshot.paramMap.get('id');
        this.subscriptions.add(
            this.userService.getUserId().subscribe((userId: string) =>{
                this.userId = userId;
                this.quizService.getQuizStatus(userId, quizId!).subscribe((status: QuizStatus) => {
                    this.quizStatus = status;
                    if (this.quizStatus == QuizStatus.Started) this.getQuestion(this.userId, quizId!);
                });
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
                this.quizStatus = QuizStatus.Started;
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
                });
            }
        )
    }

    submitAnswer(event: Event) {
        event.preventDefault();
        this.questionService.postAnswer(this.quiz.id, this.question.id, Array.from(this.selectedOptions)).subscribe(() => {
            this.displayAnswer();
            setTimeout(() => {
                this.quizService.getQuizStatus(this.userId, this.quiz.id).subscribe((status: QuizStatus) => {
                    if (status == QuizStatus.Started) this.getQuestion(this.userId, this.quiz.id);
                    if (status == QuizStatus.Completed) this.displayQuizResults();
                    if (status == QuizStatus.Abandoned) this.displayAbandonedQuizError();
                });
                this.selectedOptions.clear();
                this.quizQuestionComponent.hideCorrectOption();
            }, 2000);
        });
    }

    onSelectionChange(selectedOptions: Set<number>) {
        this.selectedOptions = selectedOptions;
    }

    displayAnswer() {
        this.questionService.getAnswerByQuestionId(this.quiz.id, this.question).subscribe(value => {
            this.question = value;
            this.quizQuestionComponent.displayCorrectOption();
        });
    }

    displayQuizResults() {
        this.quizStatus = QuizStatus.Completed;
        this.displayRecap = true;
    }

    displayAbandonedQuizError() {
        //this.dialog.open(ErrorDialogComponent, {data: {message: 'You have been inactive for too long. The quiz has been abandoned. Please start again.'}});
    }

    navigateHome() {
        this.router.navigate(['/']);
    }

    displayQuizDetails() {
        this.quizStatus = QuizStatus.notInitialized;
    }
}

import { Routes } from '@angular/router';
import {HomePage} from './pages/home/home.page';
import {QuizDetailsPage} from './pages/quiz-details/quiz-details.page';

export const routes: Routes = [
    { path: '', component: HomePage },
    { path: 'quiz/:id', component: QuizDetailsPage },
];

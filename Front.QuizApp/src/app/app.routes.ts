import { Routes } from '@angular/router';
import {HomePage} from './pages/home/home.page';
import {QuizDetailsPage} from './pages/quiz-details/quiz-details.page';
import {LoginPage} from './pages/login/login.page';

export const routes: Routes = [
    { path: '', component: HomePage },
    { path: 'login', component: LoginPage },
    { path: 'quiz/:id', component: QuizDetailsPage },
];

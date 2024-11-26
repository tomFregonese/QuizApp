import {Component, input} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    RouterOutlet,
    DashboardComponent
  ],
  templateUrl: './home.page.html',
  styleUrl: './home.page.scss'
})
export class HomePage {

  protected readonly id = input.required<string>()



}

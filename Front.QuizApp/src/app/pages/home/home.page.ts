import {Component, input} from '@angular/core';
import {DashboardComponent} from '../../components/dashboard/dashboard.component';
import {UserHeaderComponent} from '../../components/user-header/user-header.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
    imports: [
        DashboardComponent,
        UserHeaderComponent
    ],
  templateUrl: './home.page.html',
  styleUrl: './home.page.scss'
})
export class HomePage {

  protected readonly id = input.required<string>()

}

import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../../business/services/user.service';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './login.page.html',
  styleUrl: './login.page.scss'
})
export class LoginPage implements OnInit {
    email: string = '';

    constructor(private service: UserService, private router: Router) {}

    ngOnInit() {
        if (this.service.isUserLoggedIn()) {
            this.router.navigate(['']);
        }
    }

    onSubmit() {
        this.service.getUserByEmail(this.email)
            .subscribe(user => {
                if (user) {
                    localStorage.setItem('user', JSON.stringify(user));
                    this.router.navigate(['']);
                    return;
                }
          });
    }

}

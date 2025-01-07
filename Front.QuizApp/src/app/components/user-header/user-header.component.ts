import {Component, OnInit} from '@angular/core';
import {User} from '../../business/models/user.model';
import {Router} from '@angular/router';
import {UserService} from '../../business/services/user.service';

@Component({
    selector: 'app-user-header',
    standalone: true,
    imports: [],
    templateUrl: './user-header.component.html',
    styleUrl: './user-header.component.scss'
})
export class UserHeaderComponent implements OnInit {
    user: User | null = null;

    constructor(private readonly userService: UserService, private router: Router) {}

    ngOnInit() {
        const userData = localStorage.getItem('user');
        if (userData) {
            this.user = JSON.parse(userData);
        }
    }

    navigateToLogin() {
        this.router.navigate(['/login']);
    }

    logout() {
        this.userService.logout();
        this.user = null;
    }

}

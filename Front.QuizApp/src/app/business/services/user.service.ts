import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, map, Observable, of} from 'rxjs';
import {environment} from '../../environment/environment';
import {User} from '../models/user.model';
import {UserDto} from '../dtos/user.dto';
import {UserMapper} from '../mappers/user.mapper';

@Injectable({
  providedIn: 'root'
})
export class UserService {
    private readonly userApiUrl: string = environment.apiUrl;
    private userSubject: BehaviorSubject<User> = new BehaviorSubject<User>(JSON.parse(<string>localStorage.getItem('user')));

    constructor( private readonly  mapper: UserMapper,
               private readonly httpClient: HttpClient) {}

    public isUserLoggedIn(): boolean {
        return localStorage.getItem('user') !== null;
    }

    public logout(): void {
        localStorage.removeItem('user');
    }

    public getUserByEmail(email: string): Observable<User> {
        return this.httpClient.get<UserDto>(this.userApiUrl + 'user/' + email)
            .pipe(
                map((dto: UserDto) => {
                    return this.mapper.mapUserFromApiToModel(dto)
                }),
            );
    }

    getUserId(): Observable<string> {
        return of(this.userSubject.value.id);
    }
}

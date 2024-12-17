import {Injectable} from '@angular/core';
import {UserDto} from '../dtos/user.dto';
import {User} from '../models/user.model';

@Injectable({providedIn: 'root'})
export class UserMapper {
    public mapUserFromApiToModel(apiUser: UserDto): User {
        return {
            id: apiUser.id,
            firstName: apiUser.firstName,
            lastName: apiUser.lastName,
            email: apiUser.email,
        };
    }
}

import {Category} from './category.model';
import {Question} from './question.model';

export interface Quiz {
    id: string;
    name: string;
    description: string;
    difficulty: number;
    creationDate: Date;
    categoryId: string;
}

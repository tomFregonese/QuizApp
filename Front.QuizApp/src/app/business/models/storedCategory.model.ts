import {Category} from './category.model';

export interface StoredCategory {
    lastUpdated: number;
    categories: Category[];
}

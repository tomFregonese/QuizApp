import {Injectable} from '@angular/core';
import {Category} from '../models/category.model';
import {CategoryDto} from '../dtos/category.dto';

@Injectable({providedIn: 'root'})
export class CategoryMapper {
    public mapCategoryFromApiToModel(apiCategory: CategoryDto): Category {
        return {
            id: apiCategory.id,
            name: apiCategory.name,
            description: apiCategory.description || '',
        };
    }

    public mapCategoryName(categoryName: Category) : string {
        return categoryName.name;
    }
}

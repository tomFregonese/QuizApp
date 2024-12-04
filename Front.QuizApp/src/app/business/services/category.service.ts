import { Injectable } from '@angular/core';
import {map, Observable} from 'rxjs';
import {Category} from '../models/category.model';
import {HttpClient} from '@angular/common/http';
import {CategoryMapper} from './mappers/category.mapper';
import {environment} from '../../environment/environment';
import {CategoryDto} from './dtos/category.dto';
import {StoredCategory} from '../models/storedCategory.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private readonly categoryApiUrl: string = environment.apiUrl;

  constructor(private readonly  mapper: CategoryMapper,
              private readonly httpClient: HttpClient) { }

  private fetchCategories(): Observable<Category[]> {
      return this.httpClient.get<Category[]>(this.categoryApiUrl + 'fetch-categories')
          .pipe(
              map((dtos: Category[]) => {
                  return dtos.map((dtos: CategoryDto) : Category => this.mapper.mapCategoryFromApiToModel(dtos));
              })
          );
  }

  public updateCategories(): void {
    this.fetchCategories().subscribe((categories: Category[]) => {
        const categoriesToStore: StoredCategory = {
            categories: categories,
            lastUpdated: new Date().getTime()
        }
        localStorage.setItem('categories', JSON.stringify(categoriesToStore));
    });
  }

  private areCategoriesOutdated(): boolean {
    const storedCategories: string | null = localStorage.getItem('categories');
    if (storedCategories === null) {
        return true;
    }
    const categories: StoredCategory = JSON.parse(storedCategories);
    return categories.lastUpdated + 86400000 < new Date().getTime(); // 24 hours
  }

  public getCategoryNameById(categoryId: string): string {
    if (this.areCategoriesOutdated()) {
        this.updateCategories();
    }
    const storedCategories: string | null = localStorage.getItem('categories');
    if (storedCategories === null) {
        return '';
    }
    const categories: StoredCategory = JSON.parse(storedCategories);
    const category: Category | undefined = categories.categories.find((category: Category) => category.id === categoryId);
    return category ? this.mapper.mapCategoryName(category) : '';
  }

}

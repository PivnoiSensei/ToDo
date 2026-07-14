import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Category } from '@shared/models/categories/category';
import { CreateCategoryRequest } from '@shared/models/categories/create-category-request';
import { UpdateCategoryRequest } from '@shared/models/categories/update-category-request';

import { API_URL } from '@core/api';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {

    private readonly http = inject(HttpClient);

    getAll(): Observable<Category[]> {

        return this.http.get<Category[]>(
            `${API_URL}/categories`
        );

    }

    create(request: CreateCategoryRequest): Observable<Category> {

        return this.http.post<Category>(
            `${API_URL}/categories`,
            request
        );

    }

    update(request: UpdateCategoryRequest): Observable<Category> {

        return this.http.put<Category>(
            `${API_URL}/categories/${request.id}`,
            request
        );

    }

    delete(id: string): Observable<void> {

        return this.http.delete<void>(
            `${API_URL}/categories/${id}`
        );

    }

}
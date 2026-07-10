import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';

import { Observable } from 'rxjs';

import { API_URL } from '@core/api';

import { Task } from '@shared/models/tasks/task';
import { CreateTaskRequest } from '@shared/models/tasks/create-task-request';
import { UpdateTaskRequest } from '@shared/models/tasks/update-task-request';
import { PagedResult } from '@shared/models/tasks/paged-result';
import { TaskQuery } from '@shared/models/tasks/task-query';

@Injectable({
    providedIn: 'root'
})
export class TaskService {

    private readonly http = inject(HttpClient);

    getAll(query: TaskQuery): Observable<PagedResult<Task>> {

        let params = new HttpParams()
            .set(
                'page',
                query.page
            )
            .set(
                'pageSize',
                query.pageSize
            );
    
    
        if (query.search) {
    
            params = params.set(
                'search',
                query.search
            );
    
        }
    
    
        if (query.categoryId) {
    
            params = params.set(
                'categoryId',
                query.categoryId
            );
    
        }
    
    
        return this.http.get<PagedResult<Task>>(
            `${API_URL}/tasks`,
            {
                params
            }
        );
    
    }

    create(request: CreateTaskRequest): Observable<Task> {

        return this.http.post<Task>(
            `${API_URL}/tasks`,
            request
        );

    }

    update(request: UpdateTaskRequest): Observable<Task> {

        return this.http.put<Task>(
            `${API_URL}/tasks/${request.id}`,
            request
        );

    }

    delete(id: string): Observable<void> {

        return this.http.delete<void>(
            `${API_URL}/tasks/${id}`
        );

    }

}
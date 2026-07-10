import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';

import { Navbar } from '@features/tasks/components/navbar/navbar';
import { Toolbar } from '@features/tasks/components/toolbar/toolbar';
import { TaskCard } from '@features/tasks/components/task-card/task-card';
import { TaskFormModal } from '@features/tasks/components/task-form-modal/task-form-modal';
import { DeleteDialog } from '@features/tasks/components/delete-dialog/delete-dialog';

import { CategorySidebar } from '@features/categories/components/category-sidebar/category-sidebar';
import { CategoryFormModal } from '@features/categories/components/category-form-modal/category-form-modal';

import { TaskService } from '@core/services/task.service';
import { CategoryService } from '@core/services/category.service';
import { Auth } from '@core/services/auth';

import { Task } from '@shared/models/tasks/task';
import { Category } from '@shared/models/categories/category';

import { CreateTaskRequest } from '@shared/models/tasks/create-task-request';
import { UpdateTaskRequest } from '@shared/models/tasks/update-task-request';

import { CreateCategoryRequest } from '@shared/models/categories/create-category-request';
import { UpdateCategoryRequest } from '@shared/models/categories/update-category-request';

@Component({
    selector: 'app-tasks',
    standalone: true,
    imports: [
        Navbar,
        Toolbar,
        TaskCard,
        TaskFormModal,
        DeleteDialog,
        CategorySidebar,
        CategoryFormModal
    ],
    templateUrl: './tasks.html'
})
export class Tasks implements OnInit {


    private readonly taskService = inject(TaskService);

    private readonly categoryService = inject(CategoryService);

    private readonly authService = inject(Auth);

    private readonly router = inject(Router);
    private readonly cdr = inject(ChangeDetectorRef);

    selectedCategoryForEdit: Category | null = null;

    showCategoryModal = false;

    showDeleteCategoryDialog = false;

    categoryToDelete: string | null = null;

    tasks: Task[] = [];

    categories: Category[] = [];



    search = '';

    selectedCategoryId = '';



    page = 1;

    pageSize = 10;

    totalPages = 0;



    showTaskModal = false;

    selectedTask: Task | null = null;



    showDeleteDialog = false;

    taskToDelete: string | null = null;



    ngOnInit(): void {

        this.loadCategories();

        this.loadTasks();

    }



    loadTasks(): void {

        this.taskService.getAll({

            page: this.page,

            pageSize: this.pageSize,

            search: this.search || undefined,

            categoryId: this.selectedCategoryId || undefined

        })
        .subscribe({

          next: result => {

            this.tasks = result.items;
        
            this.totalPages = Math.ceil(
                result.totalCount / result.pageSize
            );
        
            this.cdr.detectChanges();
        
          }

        });

    }



    loadCategories(): void {

        this.categoryService.getAll()
            .subscribe({
    
                next: categories => {
    
                    this.categories = categories;
    
                    if (
                        this.selectedCategoryId &&
                        !categories.some(
                            c => c.id === this.selectedCategoryId
                        )
                    ) {
    
                        this.selectedCategoryId = '';
    
                        this.loadTasks();
    
                    }
    
                    this.cdr.detectChanges();
    
                }
    
            });
    
    }



    onSearchChanged(value: string): void {

        this.search = value;

        this.page = 1;

        this.loadTasks();

    }



    onCategoryChanged(value: string): void {

        this.selectedCategoryId = value;

        this.page = 1;

        this.loadTasks();

    }



    nextPage(): void {

        if (this.page < this.totalPages) {

            this.page++;

            this.loadTasks();

        }

    }



    previousPage(): void {

        if (this.page > 1) {

            this.page--;

            this.loadTasks();

        }

    }


    changePageSize(size: number): void {

      this.pageSize = size;
  
      this.page = 1;
  
      this.loadTasks();
  
    }

    openCreate(): void {

        this.selectedTask = null;

        this.showTaskModal = true;

    }



    openEdit(id: string): void {

        this.selectedTask =
            this.tasks.find(t => t.id === id) ?? null;


        this.showTaskModal = true;

    }



    closeTaskModal(): void {

        this.showTaskModal = false;

    }



    createTask(request: CreateTaskRequest): void {

        this.taskService.create(request)
            .subscribe({

                next: () => {

                    this.closeTaskModal();

                    this.loadTasks();

                }

            });

    }



    updateTask(request: UpdateTaskRequest): void {

        this.taskService.update(request)
            .subscribe({

                next: () => {

                    this.closeTaskModal();

                    this.loadTasks();

                }

            });

    }



    openDelete(id: string): void {

        this.taskToDelete = id;

        this.showDeleteDialog = true;

    }



    closeDelete(): void {

        this.showDeleteDialog = false;

        this.taskToDelete = null;

    }



    deleteTask(): void {

        if (!this.taskToDelete) {

            return;

        }


        this.taskService.delete(this.taskToDelete)
            .subscribe({

                next: () => {

                    this.closeDelete();

                    this.loadTasks();

                }

            });

    }

    openCreateCategory(): void {

        this.selectedCategoryForEdit = null;
    
        this.showCategoryModal = true;
    
    }
    
    
    openEditCategory(id: string): void {
    
        this.selectedCategoryForEdit =
            this.categories.find(c => c.id === id) ?? null;
    
        this.showCategoryModal = true;
    
    }
    
    
    closeCategoryModal(): void {

        this.showCategoryModal = false;
    
        this.selectedCategoryForEdit = null;
    
    }
    
    
    openDeleteCategory(id: string):void{
    
        this.categoryToDelete = id;
    
        this.showDeleteCategoryDialog = true;
    
    }
    
    
    closeDeleteCategory():void{
    
        this.categoryToDelete = null;
    
        this.showDeleteCategoryDialog = false;
    
    }

    createCategory(request: CreateCategoryRequest): void {

        this.categoryService.create(request)
            .subscribe({
    
                next: category => {

                    this.categories = [
                
                        ...this.categories,
                
                        category
                
                    ];
                
                    this.closeCategoryModal();
                
                }
    
            });
    
    }
    
    
    updateCategory(request: UpdateCategoryRequest): void {
    
        this.categoryService.update(request)
            .subscribe({
    
                next: category => {

                    this.categories = this.categories.map(c =>
                        c.id === category.id ? category : c
                    );
                
                    this.closeCategoryModal();
                
                }
            });
    
    }
    
    
    deleteCategory(): void {
    
        if (!this.categoryToDelete) {
    
            return;
    
        }
    
        this.categoryService.delete(this.categoryToDelete)
            .subscribe({
    
                next: () => {
    
                    this.categories = this.categories.filter(
                        c => c.id !== this.categoryToDelete
                    );
                
                    this.closeDeleteCategory();
                
                    this.loadTasks();
                
    
                }
    
            });
    
    }

    logout(): void {

        this.authService.logout()
            .subscribe({

                next: () => {

                    this.router.navigate(['/login']);

                }

            });

    }


}
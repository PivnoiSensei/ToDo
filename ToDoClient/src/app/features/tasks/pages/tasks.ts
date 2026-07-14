import { Component, OnInit, inject, signal } from '@angular/core';
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

//Data interfaces
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

    //Data lists
    readonly tasks = signal<Task[]>([]);
    readonly categories = signal<Category[]>([]);

    //Pagination, search
    readonly totalPages = signal<number>(1);
    readonly page = signal<number>(1);
    readonly pageSize = signal<number>(10);
    readonly search = signal<string>('');
    readonly selectedCategoryId = signal<string>('');

    //Modal windows states
    readonly showTaskModal = signal<boolean>(false);
    readonly showCategoryModal = signal<boolean>(false);
    readonly showDeleteDialog = signal<boolean>(false);
    readonly showDeleteCategoryDialog = signal<boolean>(false);

    //Selected elements for update/delete
    readonly selectedTask = signal<Task | null>(null);
    readonly selectedCategoryForEdit = signal<Category | null>(null);
    
    readonly currentTaskToDelete = signal<Task | null>(null);
    readonly currentCategoryToDelete = signal<Category | null>(null);

    ngOnInit(): void {
        this.loadCategories();
        this.loadTasks();
    }

    loadTasks(): void {
        this.taskService.getAll({
            page: this.page(),
            pageSize: this.pageSize(),
            search: this.search() || undefined,
            categoryId: this.selectedCategoryId() || undefined
        })
        .subscribe({
          next: result => {
            this.tasks.set(result.items);
            this.totalPages.set(Math.ceil(
                result.totalCount / result.pageSize
            ));
          }
        });
    }

    loadCategories(): void {
        this.categoryService.getAll()
            .subscribe({
                next: categories => {
                    this.categories.set(categories);
                    if (
                        this.selectedCategoryId() &&
                        !categories.some(c => c.id === this.selectedCategoryId())
                    ) {
                        this.selectedCategoryId.set('');
                        this.loadTasks();
                    }
                }
            });
    }

    //Search + pagination methods

    onSearchChanged(value: string): void {
        this.search.set(value);
        this.page.set(1);
        this.loadTasks();
    }

    onCategoryChanged(value: string): void {
        this.selectedCategoryId.set(value);
        this.page.set(1);
        this.loadTasks();

    }

    nextPage(): void {
        if (this.page < this.totalPages) {
            this.page.set(this.page() + 1);
            this.loadTasks();
        }
    }

    previousPage(): void {
        if (this.page() > 1) {
            this.page.set(this.page() -1);
            this.loadTasks();
        }
    }

    changePageSize(size: number): void {
      this.pageSize.set(size);
      this.page.set(1);
      this.loadTasks();
    }

    //Tasks

    openCreate(): void {
        this.selectedTask.set(null);
        this.showTaskModal.set(true);
    }

    openEdit(id: string): void {
        this.selectedTask.set(
            this.tasks().find(t => t.id === id) ?? null);
        this.showTaskModal.set(true);
    }

    openDelete(id: string): void {
        const foundTask = this.tasks().find(t => t.id === id) ?? null;
        this.currentTaskToDelete.set(foundTask);
        this.showDeleteDialog.set(true);
    }

    closeTaskModal(): void {
        this.showTaskModal.set(false);
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

    closeDelete(): void {
        this.showDeleteDialog.set(false);
        this.currentTaskToDelete.set(null);
    }

    deleteTask(): void {
        const task = this.currentTaskToDelete();
        if (!task) return;
    
        this.taskService.delete(task.id).subscribe({
            next: () => {
                this.closeDelete();
                this.loadTasks();
            }
        });
    }

    //Categories

    openCreateCategory(): void {
        this.selectedCategoryForEdit.set(null);
        this.showCategoryModal.set(true);
    }
    
    openEditCategory(id: string): void {
        this.selectedCategoryForEdit.set(
            this.categories().find(c => c.id === id) ?? null);
        this.showCategoryModal.set(true);
    }
    
    closeCategoryModal(): void {
        this.showCategoryModal.set(false);        
        this.selectedCategoryForEdit.set(null);
    }
    
    openDeleteCategory(id: string): void {
        const foundCategory = this.categories().find(c => c.id === id) ?? null;
        this.currentCategoryToDelete.set(foundCategory);
        this.showDeleteCategoryDialog.set(true);
    }
    
    closeDeleteCategory(): void {
        this.showDeleteCategoryDialog.set(false);
        this.currentCategoryToDelete.set(null);
    }

    createCategory(request: CreateCategoryRequest): void {
        this.categoryService.create(request).subscribe({
                next: category => {
                    this.categories.update(current => [...current, category]);
                    this.closeCategoryModal();
                }
            });
    }
    
    updateCategory(request: UpdateCategoryRequest): void {
        this.categoryService.update(request)
            .subscribe({
                next: category => {
                    this.categories.update(current =>
                         current.map(c => c.id === category.id ? category : c)
                    ); 
                    this.closeCategoryModal();
                }
            });
    }
    
    deleteCategory(): void {
        const category = this.currentCategoryToDelete();
        if (!category) return;
    
        this.categoryService.delete(category.id).subscribe({
            next: () => {
                this.categories.update(current => current.filter(c => c.id !== category.id));
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
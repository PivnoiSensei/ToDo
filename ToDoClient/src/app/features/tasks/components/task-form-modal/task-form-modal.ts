import {
    Component,
    OnChanges,
    SimpleChanges,
    input,
    output,
    inject
  } from '@angular/core';
  
  import {
    FormBuilder,
    ReactiveFormsModule,
    Validators
  } from '@angular/forms';
  
  import { Category } from '@shared/models/categories/category';
  import { Task } from '@shared/models/tasks/task';
  import { CreateTaskRequest } from '@shared/models/tasks/create-task-request';
  import { UpdateTaskRequest } from '@shared/models/tasks/update-task-request';
  
  @Component({
    selector: 'app-task-form-modal',
    standalone: true,
    imports: [
      ReactiveFormsModule
    ],
    templateUrl: './task-form-modal.html'
  })
  export class TaskFormModal implements OnChanges {
  
    private readonly fb = inject(FormBuilder);
  
    visible = input.required<boolean>();
  
    categories = input.required<Category[]>();
  
    task = input<Task | null>(null);
  
    close = output<void>();
  
    create = output<CreateTaskRequest>();
  
    update = output<UpdateTaskRequest>();
  
    readonly form = this.fb.nonNullable.group({
  
      title: ['', Validators.required],
  
      description: [''],
  
      dueDate: [''],
  
      categoryId: [''],
  
      isCompleted: [false]
  
    });
  
    ngOnChanges(changes: SimpleChanges): void {
  
      if (!changes['task']) {
        return;
      }
  
      const task = this.task();
  
      if (!task) {
  
        this.form.reset({
  
          title: '',
  
          description: '',
  
          dueDate: '',
  
          categoryId: '',
  
          isCompleted: false
  
        });
  
        return;
      }
  
      this.form.reset({
  
        title: task.title,
  
        description: task.description ?? '',
  
        dueDate: task.dueDate
          ? task.dueDate.substring(0, 10)
          : '',
  
        categoryId: task.categoryId ?? '',
  
        isCompleted: task.isCompleted
  
      });
  
    }
  
    submit(): void {
  
      if (this.form.invalid) {
  
        this.form.markAllAsTouched();
  
        return;
  
      }
  
      const value = this.form.getRawValue();
  
      const categoryId =
        value.categoryId === ''
          ? null
          : value.categoryId;
  
      const dueDate =
        value.dueDate === ''
          ? null
          : value.dueDate;
  
      if (this.task()) {
  
        this.update.emit({
  
          id: this.task()!.id,
  
          title: value.title.trim(),
  
          description:
            value.description.trim() || null,
  
          dueDate,
  
          categoryId,
  
          isCompleted: value.isCompleted
  
        });
  
        return;
      }
  
      this.create.emit({
  
        title: value.title.trim(),
  
        description:
          value.description.trim() || null,
  
        dueDate,
  
        categoryId
  
      });
  
    }
  
  }
import { Component, input, output } from '@angular/core';

import { Task } from '@shared/models/tasks/task';

@Component({
  selector: 'app-task-card',
  standalone: true,
  templateUrl: './task-card.html'
})
export class TaskCard {
  task = input.required<Task>();
  edit = output<string>();
  delete = output<string>();
}
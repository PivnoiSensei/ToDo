import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-delete-dialog',
  standalone: true,
  templateUrl: './delete-dialog.html'
})
export class DeleteDialog {

  visible = input.required<boolean>();

  close = output<void>();

  confirm = output<void>();

}
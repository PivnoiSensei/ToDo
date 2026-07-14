import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-delete-dialog',
  standalone: true,
  templateUrl: './delete-dialog.html'
})
export class DeleteDialog {
  visible = input.required<boolean>();

  itemName = input.required<string>();
  itemType = input<string>('item');

  close = output<void>();
  confirm = output<void>();
}
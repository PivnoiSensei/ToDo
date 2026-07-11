import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-toolbar',
    standalone: true,
    imports: [
        FormsModule
    ],
    templateUrl: './toolbar.html'
})
export class Toolbar {
    search = input.required<string>();
    searchChange = output<string>();
    create = output<void>();
}
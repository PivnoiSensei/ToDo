import {
    Component,
    effect,
    input,
    output,
    signal
} from '@angular/core';

import { FormsModule } from '@angular/forms';

import { Category } from '@shared/models/categories/category';
import { CreateCategoryRequest } from '@shared/models/categories/create-category-request';
import { UpdateCategoryRequest } from '@shared/models/categories/update-category-request';

@Component({
    selector: 'app-category-form-modal',
    standalone: true,
    imports: [
        FormsModule
    ],
    templateUrl: './category-form-modal.html'
})
export class CategoryFormModal {

    visible = input.required<boolean>();

    category = input<Category | null>(null);

    close = output<void>();

    create = output<CreateCategoryRequest>();

    update = output<UpdateCategoryRequest>();

    name = signal('');

    constructor() {

        effect(() => {

            this.name.set(
                this.category()?.name ?? ''
            );

        });

    }

    save(): void {

        const value = this.name().trim();
    
        if (!value) {
            return;
        }
    
        const category = this.category();
    
        if (category) {
    
            this.update.emit({
    
                id: category.id,
    
                name: value
    
            });
    
        }
        else {
    
            this.create.emit({
    
                name: value
    
            });
    
        }
    
        this.name.set('');
    
    }

}
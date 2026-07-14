import {
    Component,
    effect,
    input,
    output,
    inject,
    untracked
} from '@angular/core';

import { FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';

import { Category } from '@shared/models/categories/category';
import { CreateCategoryRequest } from '@shared/models/categories/create-category-request';
import { UpdateCategoryRequest } from '@shared/models/categories/update-category-request';

@Component({
    selector: 'app-category-form-modal',
    standalone: true,
    imports: [
        ReactiveFormsModule
    ],
    templateUrl: './category-form-modal.html'
})
export class CategoryFormModal{
    private readonly fb = inject(FormBuilder)
    visible = input.required<boolean>();
    category = input<Category | null>(null);
    close = output<void>();
    create = output<CreateCategoryRequest>();
    update = output<UpdateCategoryRequest>();

    readonly form = this.fb.nonNullable.group({
        name: ['', Validators.required]
    });

    constructor(){
        effect(() => {
            const category = this.category();
    
            untracked(()=>{
                if(!category){
                    this.form.reset({
                        name: ''
                    })
        
                    return;
                }

                this.form.reset({
                    name: category.name
                })
            })
        });
        
    }

    submit(): void{
        if(this.form.invalid){
            this.form.markAllAsTouched();
            return;
        }

        const value = this.form.getRawValue();

        if(this.category()){
            this.update.emit({
                id: this.category()!.id,
                name: value.name.trim()
            })

            return;
        }

        this.create.emit({
            name: value.name.trim()
        })

    }

}
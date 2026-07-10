import { Component, input, output } from '@angular/core';

import { Category } from '@shared/models/categories/category';

@Component({
  selector: 'app-category-sidebar',
  standalone: true,
  templateUrl: './category-sidebar.html'
})
export class CategorySidebar {

  categories = input.required<Category[]>();

  selectedCategoryId = input.required<string>();

  selectCategory = output<string>();

  createCategory = output<void>();

  editCategory = output<string>();

  deleteCategory = output<string>();

}
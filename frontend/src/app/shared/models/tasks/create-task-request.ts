export interface CreateTaskRequest {
    title: string;
    description?: string |null;
    dueDate?: string | null;
    categoryId?: string | null;
}
export interface UpdateTaskRequest {
    id: string;
    title: string;
    description?: string | null;
    isCompleted: boolean;
    dueDate?: string | null;
    categoryId?: string | null;
}
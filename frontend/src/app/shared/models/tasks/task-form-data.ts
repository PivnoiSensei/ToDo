import { Task } from './task';

export interface TaskFormData {
    isEdit: boolean;
    task?: Task;
}
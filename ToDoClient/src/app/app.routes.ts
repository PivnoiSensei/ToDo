import { Routes } from '@angular/router';

import { Login } from './features/login/login';
import { Register } from './features/register/register';
import { Tasks } from './features/tasks/pages/tasks';

import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
    {
        path:'',
        redirectTo:'login',
        pathMatch:'full'
    },
    {
        path:'login',
        component:Login
    },
    {
        path:'register',
        component:Register
    },
    {
        path:'tasks',
        component:Tasks,
        canActivate: [authGuard]
    },
    {
        path:'**',
        redirectTo:'login'
    }
];

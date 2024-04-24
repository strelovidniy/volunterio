import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import AuthGuard from '../core/guards/auth.guard';
import RouteGuard from '../core/guards/route.guard';

import RequestsComponent from './requests.component';


@NgModule({
    imports: [
        RouterModule.forChild([{
            path: '',
            canActivate: [AuthGuard],
            children: [
                { path: '', component: RequestsComponent, canActivate: [RouteGuard] },
            ]
        }])
    ],
    exports: [
        RouterModule
    ],
    providers: [AuthGuard]
})
export default class RequestsRouterModule { }

import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import AuthGuard from '../core/guards/auth.guard';
import RouteGuard from '../core/guards/route.guard';

import RequestsComponent from './requests.component';
import RequestComponent from './request/request.component';
import RequestDetailsComponent from './request-details/request-details.component';

import Role from '../core/enums/role/role.enum';


@NgModule({
    imports: [
        RouterModule.forChild([{
            path: '',
            canActivate: [AuthGuard],
            children: [
                { path: '', component: RequestsComponent, canActivate: [RouteGuard] },
                { path: 'update', component: RequestComponent, canActivate: [RouteGuard], data: { roles: [Role.canCreateHelpRequest] } },
                { path: 'create', component: RequestComponent, canActivate: [RouteGuard], data: { roles: [Role.canCreateHelpRequest] } },
                { path: 'details', component: RequestDetailsComponent, canActivate: [RouteGuard] },
            ]
        }])
    ],
    exports: [
        RouterModule
    ],
    providers: [AuthGuard]
})
export default class RequestsRouterModule { }

import { NgModule } from '@angular/core';

import AdminRouterModule from './requests.router.module';
import SharedModule from '../shared/shared.module';

import RequestsComponent from './requests.component';
import RequestComponent from './request/request.component';
import RequestDetailsComponent from './request-details/request-details.component';


@NgModule({
    imports: [
        SharedModule,
        AdminRouterModule,
    ],
    declarations: [
        RequestsComponent,
        RequestComponent,
        RequestDetailsComponent
    ],
})
export default class RequestsModule { }

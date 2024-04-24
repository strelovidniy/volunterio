import { NgModule } from '@angular/core';

import AdminRouterModule from './requests.router.module';
import SharedModule from '../shared/shared.module';

import RequestsComponent from './requests.component';


@NgModule({
    imports: [
        SharedModule,
        AdminRouterModule,
    ],
    declarations: [
        RequestsComponent
    ],
})
export default class RequestsModule { }

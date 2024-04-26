import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import EndpointService from './endpoint.service';
import IHelpRequest from '../interfaces/help-request/help-request.interface';
import ICreateHelpRequestRequest from '../interfaces/help-request/create-help-request-request.interface';
import IUpdateHelpRequestRequest from '../interfaces/help-request/update-help-request-request.interface';
import IPagedCollectionView from '../interfaces/system/paged-collection-view.interface';


@Injectable({
    providedIn: 'root'
})
export default class HelpRequestService {
    public constructor(
        private readonly http: HttpClient,
        private readonly endpointService: EndpointService
    ) { }

    public getHelpRequests(query: string, callback: (helpRequests: IPagedCollectionView<IHelpRequest>) => void, errorCallback: () => void): void {
        this.http.get<IPagedCollectionView<IHelpRequest>>(this.endpointService.getHelpRequests(query)).subscribe({
            next: (response: IPagedCollectionView<IHelpRequest>): void => {
                callback(response);
            },
            error: (): void => {
                errorCallback();
            }
        });
    }

    public createHelpRequest(request: ICreateHelpRequestRequest, callback?: () => void, errorCallback?: () => void): void {
        this.http.post(this.endpointService.createHelpRequest(), request).subscribe({
            next: (): void => {
                if (callback) {
                    callback();
                }
            },
            error: (): void => {
                if (errorCallback) {
                    errorCallback();
                }
            }
        });
    }

    public updateHelpRequest(request: IUpdateHelpRequestRequest, callback?: () => void, errorCallback?: () => void): void {
        this.http.put(this.endpointService.updateHelpRequest(), request).subscribe({
            next: (): void => {
                if (callback) {
                    callback();
                }
            },
            error: (): void => {
                if (errorCallback) {
                    errorCallback();
                }
            }
        });
    }

    public deleteHelpRequest(id: string, callback?: () => void, errorCallback?: () => void): void {
        this.http.delete(this.endpointService.deleteHelpRequest(id)).subscribe({
            next: (): void => {
                if (callback) {
                    callback();
                }
            },
            error: (): void => {
                if (errorCallback) {
                    errorCallback();
                }
            }
        });
    }

    public getHelpRequest(id: string, callback: (helpRequest: IHelpRequest) => void, errorCallback?: () => void): void {
        this.http.get<IHelpRequest>(this.endpointService.getHelpRequest(id)).subscribe({
            next: (response: IHelpRequest): void => {
                callback(response);
            },
            error: (): void => {
                if (errorCallback) {
                    errorCallback();
                }
            }
        });
    }
}

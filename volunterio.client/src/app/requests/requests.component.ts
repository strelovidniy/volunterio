import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs/internal/Subscription';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router, ActivatedRoute } from '@angular/router';
import { debounceTime, tap } from 'rxjs';

import Role from '../core/enums/role/role.enum';
import HelpRequestTableFields from '../core/enums/help-request/help-request-table-fields.enum';

import IQeryParams from '../core/interfaces/system/query-params.interface';
import IHelpRequest from '../core/interfaces/help-request/help-request.interface';
import IPagedCollectionView from '../core/interfaces/system/paged-collection-view.interface';

import AuthenticationService from '../core/services/authentication.service';
import LoaderService from '../core/services/loader.service';
import NotifierService from '../core/services/notifier.service';
import PaginationService from '../core/services/pagination.service';
import ImageService from '../core/services/placeholder.service';
import ViewService from '../core/services/view.service';
import HelpRequestService from '../core/services/help-request.service';

import ConfirmDialogComponent from '../shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import IHelpRequestImage from '../core/interfaces/help-request/help-request-image.interface';


@Component({
    templateUrl: './requests.component.html',
    styleUrls: ['./requests.component.scss', './requests-responsive.component.scss']
})
export default class RequestsComponent implements OnInit, OnDestroy {
    public displayedColumns: string[] = [
        HelpRequestTableFields.image,
        HelpRequestTableFields.title,
        HelpRequestTableFields.description,
        HelpRequestTableFields.tags,
        HelpRequestTableFields.deadline,
        HelpRequestTableFields.actions
    ];

    public tableColumnsName = HelpRequestTableFields;
    public items: IHelpRequest[] = [];

    public totalCount: number = 0;

    public pageSizeOptions: number[] = [10];
    public pageSize: number = 10;
    private pageIndex: number = 1;

    public sortFiledName: string = '';
    public sortDirection: boolean | null = null;

    private searchTerm: string = '';
    public searchNameControl = new FormControl('', []);

    public defaultImageUrl: string;

    public tableView: boolean = true;

    private searchSubscription?: Subscription;
    private queryParamsSubscription?: Subscription;
    private removeItemSubscription?: Subscription;

    constructor(
        private readonly helpRequestService: HelpRequestService,
        private readonly dialog: MatDialog,
        private readonly paginationService: PaginationService,
        private readonly notifier: NotifierService,
        private readonly loader: LoaderService,
        private readonly router: Router,
        private readonly imageService: ImageService,
        private readonly authService: AuthenticationService,
        private readonly activatedRoute: ActivatedRoute,
        private readonly location: Location,
        private readonly viewService: ViewService
    ) {
        this.defaultImageUrl = this.imageService.defaultImageUrl;
    }

    public ngOnInit(): void {

        this.tableView = this.viewService.isTableView('requests');

        this.pageSizeOptions = this.paginationService.pageSizeOptions;
        this.getData();

        this.searchSubscription = this.searchNameControl.valueChanges.pipe(
            debounceTime(500),
            tap((value: any): void => {
                this.searchTerm = value;
                this.getData();
            })).subscribe();

        this.queryParamsSubscription = this.activatedRoute.queryParams.subscribe({
            next: (params: { tag?: string }): void => {
                this.searchNameControl.setValue(params.tag || '');
                this.location.replaceState('/requests');
            }
        });
    }

    public ngOnDestroy(): void {
        this.searchSubscription?.unsubscribe();
        this.queryParamsSubscription?.unsubscribe();
        this.removeItemSubscription?.unsubscribe();
    }

    public getMainImage(item: IHelpRequest): IHelpRequestImage {
        return item.images.sort((a, b): number => (a.position || 0) - (b.position || 0))[0];
    }

    public getSecondaryImages(item: IHelpRequest): IHelpRequestImage[] {
        return item.images.sort((a, b): number => (a.position || 0) - (b.position || 0)).slice(1);
    }

    public openDetails(id: string): void {
        this.router.navigate(['/requests/details'], { queryParams: { id } });
    }

    private getData(): void {
        this.loader.show();

        const params: IQeryParams = {
            searchQuery: this.searchTerm,
            pageNumber: this.pageIndex,
            pageSize: this.pageSize,
            sortBy: this.sortFiledName,
            sortAscending: this.sortDirection,
            expandProperty: 'images, issuer.details'
        };

        const query = this.paginationService.queryBuilder(params);

        this.helpRequestService.getHelpRequests(
            query,
            (helpRequests: IPagedCollectionView<IHelpRequest>): void => {
                this.items = helpRequests.items;
                this.totalCount = helpRequests.totalCount;

                this.loader.hide();
            },
            (): void => {
                this.loader.hide();
            }
        );
    }

    public handlePageEvent(event: PageEvent): any {
        const { pageSize, pageIndex } = event;

        this.pageSize = pageSize;
        this.pageIndex = pageIndex + 1;

        this.getData();
    }

    public sortChange(sortState: Sort): void {
        const { active, direction } = sortState;

        this.sortDirection = this.paginationService.getSortDirection(direction);
        this.sortFiledName = this.sortDirection !== null ? active : '';
        this.getData();

    }

    public search(): void {
        this.pageIndex = 1;

        this.getData();
    }

    public createHelpRequest(): void {
        this.router.navigate(['/requests/create']);
    }

    public editHelpRequest(id: string): void {
        this.router.navigate(['/requests/update'], { queryParams: { id } });
    }

    public selectTag(tag: string, event: MouseEvent): void {
        event.stopPropagation();

        if (this.searchNameControl.value === tag) {
            this.searchNameControl.setValue('');
        } else {
            this.searchNameControl.setValue(tag);
        }
    }

    public isTagActive(tag: string): boolean {
        return this.searchNameControl.value === tag;
    }

    public removeItem(id: string): void {
        this.removeItemSubscription = this.dialog.open(ConfirmDialogComponent, {
            maxWidth: '400px',
            data: {
                message: $localize`Are you sure you want to remove this request? This action can not be undone.`
            }
        }).afterClosed().subscribe((confirm: boolean): void => {
            if (confirm) {
                this.loader.show();

                this.helpRequestService.deleteHelpRequest(
                    id,
                    (): void => {
                        this.notifier.success($localize`Request removed successfully`);

                        this.getData();
                    },
                    (): void => {
                        this.notifier.error($localize`Something went wrong. Request was not removed.`);

                        this.loader.hide();
                    }
                );
            }
        });
    }

    public switchView(): void {
        this.tableView = !this.tableView;
        this.viewService.setTableView('requests', this.tableView);
    }

    public getSwitchViewButtonText(): string {
        return this.tableView ? $localize`Switch to Card View` : $localize`Switch to Table View`;
    }

    public getIssuerImageUrl(item: IHelpRequest): string {
        return item.issuerImage || this.defaultImageUrl;
    }

    public getIssuerImageThumbnailUrl(item: IHelpRequest): string {
        return item.issuerImageThumbnail || this.defaultImageUrl;
    }

    public selectIssuer(issuerName: string, event: MouseEvent): void {
        event.stopPropagation();

        if (this.searchNameControl.value === issuerName) {
            this.searchNameControl.setValue('');
        } else {
            this.searchNameControl.setValue(issuerName);
        }
    }

    public get canCreateHelpRequest(): boolean {
        return this.authService.checkRole(Role.canCreateHelpRequest);
    }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { Location } from '@angular/common';

import LoaderService from 'src/app/core/services/loader.service';
import ImageService from 'src/app/core/services/placeholder.service';
import HelpRequestService from 'src/app/core/services/help-request.service';
import IHelpRequest from 'src/app/core/interfaces/help-request/help-request.interface';


@Component({
    templateUrl: './request-details.component.html',
    styleUrls: ['./request-details.component.scss', './request-details-responsive.component.scss']
})
export default class RequestDetailsComponent implements OnInit, OnDestroy {
    public helpRequest: IHelpRequest = {} as IHelpRequest;

    private id?: string;

    private queryParamsSubscription?: Subscription;

    public defaultImageUrl: string;

    public slideConfig = {
        'slidesToShow': 4,
        'slidesToScroll': 1,
        'autoplay': true,
        'autoplaySpeed': 5000,
        'pauseOnHover': true,
        'arrows': true,
        'dots': true,
        'responsive': [
            {
                'breakpoint': 1350,
                'settings': {
                    'slidesToShow': 3,
                    'slidesToScroll': 1,
                    'autoplay': true,
                    'autoplaySpeed': 5000,
                    'pauseOnHover': true,
                    'arrows': true,
                    'dots': true
                }
            },
            {
                'breakpoint': 1140,
                'settings': {
                    'slidesToShow': 2,
                    'slidesToScroll': 1,
                    'autoplay': true,
                    'autoplaySpeed': 5000,
                    'pauseOnHover': true,
                    'arrows': true,
                    'dots': true
                }
            },
            {
                'breakpoint': 868,
                'settings': {
                    'slidesToShow': 1,
                    'slidesToScroll': 1,
                    'autoplay': true,
                    'autoplaySpeed': 5000,
                    'pauseOnHover': true,
                    'infinite': true,
                    'arrows': true,
                    'dots': true
                }
            }
        ]
    };

    constructor(
        private readonly activatedRoute: ActivatedRoute,
        private readonly router: Router,
        private readonly imageService: ImageService,
        private readonly helpRequestService: HelpRequestService,
        private readonly loader: LoaderService,
        private readonly location: Location
    ) {
        this.defaultImageUrl = this.imageService.defaultImageUrl;
    }

    public ngOnInit(): void {
        this.loader.show();

        this.queryParamsSubscription = this.activatedRoute.queryParams.subscribe({
            next: (params: { id?: string }): void => {
                this.id = params.id;

                if (!this.id) {
                    this.router.navigate(['/requests']);
                    this.loader.hide();
                } else {
                    this.helpRequestService.getHelpRequest(
                        this.id,
                        (helpRequest: IHelpRequest): void => {
                            if (!helpRequest) {
                                this.router.navigate(['/requests']);

                                return;
                            }

                            this.helpRequest = helpRequest;

                            this.loader.hide();
                        },
                        (): void => {
                            this.router.navigate(['/requests']);
                            this.loader.hide();
                        }
                    );
                }
            }
        });
    }

    public ngOnDestroy(): void {
        this.queryParamsSubscription?.unsubscribe();
    }

    public goBack(): void {
        this.location.back();
    }

    public selectTag(tag: string): void {
        this.router.navigate(['/requests'], { queryParams: { tag } });
    }
}

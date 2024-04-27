import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import ICreateHelpRequestRequest from 'src/app/core/interfaces/help-request/create-help-request-request.interface';
import IHelpRequestImage from 'src/app/core/interfaces/help-request/help-request-image.interface';
import IHelpRequest from 'src/app/core/interfaces/help-request/help-request.interface';
import IUpdateHelpRequestRequest from 'src/app/core/interfaces/help-request/update-help-request-request.interface';

import HelpRequestService from 'src/app/core/services/help-request.service';

import LoaderService from 'src/app/core/services/loader.service';
import NotifierService from 'src/app/core/services/notifier.service';


@Component({
    templateUrl: './request.component.html',
    styleUrls: ['./request.component.scss', './request-responsive.component.scss']
})
export default class RequestComponent implements OnInit, OnDestroy {
    public titleFormControl = new FormControl('', [Validators.required, Validators.maxLength(200)]);
    public descriptionFormControl = new FormControl('', [Validators.required, Validators.maxLength(2000)]);
    public deadlineFormControl = new FormControl('');
    public tagsFormControl = new FormControl('');

    public showContactInfo: boolean = false;
    public isDeadline: boolean = false;

    public images: IHelpRequestImage[] = [];

    private imageIdsToRemove: string[] = [];

    private imagesToAdd: File[] = [];

    public helpRequest: IHelpRequest | ICreateHelpRequestRequest | IUpdateHelpRequestRequest = {
        id: '',
        title: '',
        description: '',
        tags: [],
        latitude: undefined,
        longitude: undefined,
        showContactInfo: false,
        deadline: undefined,
        images: []
    } as IHelpRequest | ICreateHelpRequestRequest | IUpdateHelpRequestRequest;

    private queryParamsSubscription?: Subscription;

    constructor(
        private readonly notifier: NotifierService,
        private readonly helpRequestService: HelpRequestService,
        private readonly router: Router,
        private readonly loader: LoaderService,
        private readonly activatedRoute: ActivatedRoute,
        private readonly location: Location
    ) { }

    public ngOnInit(): void {
        this.loader.show();

        this.queryParamsSubscription = this.activatedRoute.queryParams.subscribe((params: { id?: string }): void => {
            if (!params.id) {
                this.location.replaceState('/requests/create');
                this.loader.hide();
            } else {
                this.helpRequestService.getHelpRequest(
                    params.id,
                    (helpRequest: IHelpRequest): void => {
                        if (!helpRequest) {
                            this.location.back();
                        } else {
                            this.helpRequest = helpRequest;

                            this.titleFormControl.setValue(helpRequest.title);
                            this.descriptionFormControl.setValue(helpRequest.description);
                            this.tagsFormControl.setValue(helpRequest.tags.join(', '));
                            this.showContactInfo = !!helpRequest.contactInfo;
                            this.isDeadline = !!helpRequest.deadline;
                            this.deadlineFormControl.setValue(helpRequest.deadline as any);
                            this.images = helpRequest.images;
                        }

                        this.loader.hide();
                    },
                    (): void => {
                        this.location.back();
                    }
                );
            }
        });
    }

    public ngOnDestroy(): void {
        this.queryParamsSubscription?.unsubscribe();
    }

    public save(): void {
        if ((this.helpRequest as IHelpRequest).id) {
            this.updateHelpRequest();
        } else {
            this.createHelpRequest();
        }
    }

    private updateHelpRequest(): void {
        this.loader.show();

        const request = {
            id: (this.helpRequest as IHelpRequest).id,
            title: this.titleFormControl.value,
            description: this.descriptionFormControl.value,
            tags: this.tagsFormControl.value ? this.tagsFormControl.value.split(',').map((tag: string): string => tag.trim()) : [],
            showContactInfo: this.showContactInfo,
            deadline: this.isDeadline ? this.deadlineFormControl.value : undefined,
            images: this.imagesToAdd,
            imagesToDelete: this.imageIdsToRemove
        } as IUpdateHelpRequestRequest;

        if (this.helpRequest.images.length - request.imagesToDelete.length + request.images.length <= 0) {
            this.notifier.error($localize`Please add at least one image`);

            this.loader.hide();

            return;
        }

        this.helpRequestService.updateHelpRequest(
            request,
            (): void => {
                this.notifier.success($localize`Request updated successfully`);

                this.loader.hide();

                this.router.navigate(['/requests']);
            },
            (): void => {
                this.notifier.error($localize`Failed to update request`);

                this.loader.hide();
            }
        );
    }

    private createHelpRequest(): void {
        this.loader.show();

        const request = {
            title: this.titleFormControl.value,
            description: this.descriptionFormControl.value,
            tags: this.tagsFormControl.value ? this.tagsFormControl.value.split(',').map((tag: string): string => tag.trim()) : [],
            showContactInfo: this.showContactInfo,
            deadline: this.isDeadline ? this.deadlineFormControl.value : undefined,
            images: this.imagesToAdd
        } as ICreateHelpRequestRequest;

        if (request.images.length === 0) {
            this.notifier.error($localize`Please add at least one image`);

            this.loader.hide();

            return;
        }

        this.helpRequestService.createHelpRequest(
            request,
            (): void => {
                this.notifier.success($localize`Request created successfully`);

                this.loader.hide();

                this.router.navigate(['/requests']);

            },
            (): void => {
                this.notifier.error($localize`Failed to create request`);

                this.loader.hide();
            }
        );
    }

    public removeImage(image: IHelpRequestImage): void {
        if (image.id) {
            this.imageIdsToRemove.push(image.id);
        }

        this.images = this.images.filter((i: IHelpRequestImage): boolean => i !== image);
        this.imagesToAdd = this.imagesToAdd.filter((i: File): boolean => i !== (image as any).file);
    }

    public addImages(): void {
        const input = document.createElement('input');

        input.type = 'file';
        input.accept = '.png,.jpeg,.jpg';
        input.id = 'userPictureInput';
        input.multiple = true;
        input.onchange = (event: Event): void => this.addImageFiles(event);
        input.click();
    }

    private addImageFiles(event: Event): void {
        if (!event.target) {
            return;
        }

        const input = event.target as HTMLInputElement;
        const files = input.files;

        if (!files) {
            return;
        }

        for (let i = 0; i < files.length; i++) {
            const file = files[i];

            if (file.size > 104857600) {
                this.notifier.error($localize`The image size should not exceed 100MB`);
                this.notifier.warning($localize`Images with size over 100MB ignored`);

                continue;
            }

            this.imagesToAdd.push(file);

            const reader = new FileReader();

            reader.onload = (e: ProgressEvent<FileReader>): void => {
                if (!e.target) {
                    return;
                }

                const image = {
                    id: '',
                    imageUrl: e.target.result as string,
                    imageThumbnailUrl: e.target.result as string,
                    position: 0,
                    file
                } as IHelpRequestImage;

                this.images.push(image);
            };

            reader.readAsDataURL(file);
        }


        this.removeImageInput();
    }

    private removeImageInput(): void {
        const input = document.getElementById('userPictureInput');

        if (input) {
            input.remove();
        }
    }

    public goBack(): void {
        this.location.back();
    }
}

import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { SwPush } from '@angular/service-worker';
import * as lodash from 'lodash';

import NotifierService from '../core/services/notifier.service';
import LoaderService from '../core/services/loader.service';
import AuthenticationService from '../core/services/authentication.service';

import INotificationsConfig from '../core/interfaces/notifications-config/notifications-config.interface';
import IUpdateNotificationsConfigRequest from '../core/interfaces/notifications-config/update-notifications-config-request.interface';
import NotificationsConfigService from '../core/services/notifications-config.service';


@Component({
    templateUrl: './notifications.component.html',
    styleUrls: ['./notifications.component.scss', './notifications-responsive.component.scss']
})
export default class NotificationsComponent implements OnInit {

    public notificationsConfig: INotificationsConfig | IUpdateNotificationsConfigRequest = {
        enableNotifications: false,
        enableUpdateNotifications: false,
        enableTagFilter: false,
        tagFilters: [],
        enableTitleFilter: false,
        titleFilters: []
    } as INotificationsConfig | IUpdateNotificationsConfigRequest;

    public tagFiltersFormControl: FormControl = new FormControl('', [Validators.required]);
    public titleFiltersFormControl: FormControl = new FormControl('', [Validators.required]);

    constructor(
        private readonly notifier: NotifierService,
        private readonly loader: LoaderService,
        private readonly authService: AuthenticationService,
        private readonly notificationConfigService: NotificationsConfigService,
        private readonly swPush: SwPush
    ) { }

    public ngOnInit(): void {
        if (this.authService.currentUser.notificationsConfig) {
            this.notificationsConfig = this.authService.currentUser.notificationsConfig;

            if (this.notificationsConfig.tagFilters) {
                this.tagFiltersFormControl.setValue(this.notificationsConfig.tagFilters.join(', '));
            }

            if (this.notificationsConfig.titleFilters) {
                this.titleFiltersFormControl.setValue(this.notificationsConfig.titleFilters.join(', '));
            }
        }
    }

    public setTags(event: Event): void {
        const target = event.target as HTMLInputElement;

        this.notificationsConfig.tagFilters = target.value.split(',').map((tag): string => tag.trim());
    }

    public setTitles(event: Event): void {
        const target = event.target as HTMLInputElement;

        this.notificationsConfig.titleFilters = target.value.split(',').map((title): string => title.trim());
    }

    public isButtonDisabled(): boolean {
        return this.notificationsConfig.enableNotifications === this.authService.currentUser.notificationsConfig?.enableNotifications
            || this.notificationsConfig.enableUpdateNotifications === this.authService.currentUser.notificationsConfig?.enableUpdateNotifications
            || this.notificationsConfig.enableTagFilter === this.authService.currentUser.notificationsConfig?.enableTagFilter
            || lodash.isEqual(this.notificationsConfig.tagFilters, this.authService.currentUser.notificationsConfig?.tagFilters)
            || this.notificationsConfig.enableTitleFilter === this.authService.currentUser.notificationsConfig?.enableTitleFilter
            || lodash.isEqual(this.notificationsConfig.titleFilters, this.authService.currentUser.notificationsConfig?.titleFilters);
    }

    public async saveAsync(): Promise<void> {
        this.loader.show();

        if (this.notificationsConfig.enableNotifications) {
            try {
                const pushSubscription = await this.swPush.requestSubscription({
                    serverPublicKey: 'BF4wld7aC9UXlrSesCUTuMbG8KbV-BwkdOELk3ltwwGzc02EJh_FtFv2FVMxQ1fwc8UEbPbRXYiRVNlrDsL0UF4',
                });

                await this.authService.addUserPushSubscription(pushSubscription).toPromise();
            } catch (error) {
                this.notifier.error($localize`An error occurred while subscribing to push notifications`);

                console.error($localize`Failed to subscribe to push notifications`);
                console.error(error);
            }
        }

        this.notificationConfigService.updateNotificationsConfig(
            this.notificationsConfig as IUpdateNotificationsConfigRequest,
            (): void => {
                this.loader.hide();

                this.notificationsConfig = this.authService.currentUser.notificationsConfig = this.notificationsConfig;

                this.notifier.success($localize`Your notifications settings have been updated`);
            },
            (): void => {
                this.loader.hide();

                this.notifier.error($localize`An error occurred while updating your notifications settings`);
            }
        );
    }
}

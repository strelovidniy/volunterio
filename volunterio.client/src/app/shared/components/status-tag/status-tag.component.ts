import { Component, Input } from '@angular/core';


@Component({
    selector: 'volunterio-status-tag',
    templateUrl: './status-tag.component.html',
    styleUrls: ['./status-tag.component.scss']
})
export default class StatusTagComponent {

    @Input() public status: string = ''; // TODO: i18n
    @Input() public type: string = '';


}

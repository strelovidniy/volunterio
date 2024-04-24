import { Component, EventEmitter, Input, Output } from '@angular/core';


@Component({
    selector: 'volunterio-toggle',
    templateUrl: './toggle.component.html',
    styleUrls: ['./toggle.component.scss']
})
export default class ToggleComponent {

    @Input() public label: string = '';
    @Input() public enabled: boolean = false;
    @Output() public enabledChange = new EventEmitter<boolean>();

    public toggle(): void {
        this.enabled = !this.enabled;
        this.enabledChange.emit(this.enabled);
    }
}

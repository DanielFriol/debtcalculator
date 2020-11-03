import { Component, Input } from '@angular/core';
import { AppService } from '../../app.service';

@Component({
    selector: 'app-loading',
    templateUrl: './loading.component.html',
    styleUrls: ['./loading.component.css']
})

export class LoadingComponent {
    @Input() statusLoading: boolean;

    constructor(private appService: AppService) { }
    
    status = this.appService.showLoading;
}
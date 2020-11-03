import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class NotificationService {

    constructor(private toastr: ToastrService) { }

    success(title: string, message: string) {
        this.toastr.success(message, title);
    }

    successHtmlMessage(title: string, message: string) {
        this.toastr.success(message, title, { enableHtml: true });
    }

    error(title: string, message: string) {
        this.toastr.error(message, title);
    }

    errorHtmlMessage(title: string, message: string) {
        this.toastr.error(message, title, { enableHtml: true });
    }

    info(title: string, message: string) {
        this.toastr.info(message, title);
    }

    infoHtmlMessage(title: string, message: string) {
        this.toastr.info(message, title, { enableHtml: true });
    }

    warning(title: string, message: string) {
        this.toastr.warning(message, title);
    }

    warningHtmlMessage(title: string, message: string) {
        this.toastr.warning(message, title, { enableHtml: true });
    }
}

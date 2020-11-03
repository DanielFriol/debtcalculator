import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from './storage.service';
import { finalize } from 'rxjs/operators';
import { AppService } from '../../app.service';
import { ValidateTokenService } from './validate-token.service';


@Injectable()

export class Interceptor implements HttpInterceptor {
    constructor(
        private storage: StorageService,
        private appService: AppService,
        private validateToken: ValidateTokenService,
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler):
        Observable<HttpEvent<any>> {

        this.appService.showLoading.next(true);

        this.validateToken.validateToken(req.url);

        const token = this.storage.getStorage().token;
        req = req.clone({
            setHeaders: {
                'Authorization': `bearer ${token}`
            }
        });

        return next.handle(req).pipe(
            finalize(async () => {
                this.appService.showLoading.next(false);
            })
        );
    }
}

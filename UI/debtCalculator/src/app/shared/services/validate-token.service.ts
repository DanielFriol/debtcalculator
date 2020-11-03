import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { StorageService } from './storage.service';
import { environment } from '../../../environments/environment';
import { take } from 'rxjs/operators';

@Injectable()

export class ValidateTokenService {
    constructor(
        private storage: StorageService,
        private router: Router,
        private http: HttpClient,
    ) { }

    async validateToken(reqUrl) {
        if (this.storage.getStorage().token) {
            let refreshTokenUrl = environment.apiUrl + '/Sign';
            const exp = this.storage.getExpiresDate();
            const now = new Date();

            if (exp < now) {
                this.storage.clearStorage();
                this.router.navigate(['/login']);
                return;
            }
        } else {
            this.storage.clearStorage();
            this.router.navigate(['/login']);
        }
    }
}

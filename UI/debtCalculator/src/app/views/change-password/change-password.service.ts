import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { first } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ChangePasswordModel } from '../../shared/models/login.models';


@Injectable()
export class ChangePasswordService {

    constructor(
        private http: HttpClient
    ) { }

    changePassword(request: ChangePasswordModel) {
        return this.http.post(environment.apiUrl + 'User/ChangePassword', request).pipe(first()).toPromise();
    }
}

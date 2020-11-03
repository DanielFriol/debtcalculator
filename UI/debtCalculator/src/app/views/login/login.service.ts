import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { first } from 'rxjs/operators';
import { LoginModel, SendVerificationCodeModel, ChangePasswordModel } from '../../shared/models/login.models';
import { environment } from '../../../environments/environment';

@Injectable()
export class LoginService {

  constructor(
    private http: HttpClient
  ) { }

  authenticate(request: LoginModel) {
    return this.http.post(environment.apiUrl + 'SignIn', request).pipe(first()).toPromise();
  }

  forgotPassword(request: SendVerificationCodeModel) {
    return this.http.post(environment.apiUrl + 'User/ForgotPassword', request).pipe(first()).toPromise();
  }

  changePassword(request: ChangePasswordModel) {
    return this.http.post(environment.apiUrl + 'User/ChangePassword', request).pipe(first()).toPromise();
  }

  register(request) {
    return this.http.post(environment.apiUrl + 'User', request).pipe(first()).toPromise();
  }
}

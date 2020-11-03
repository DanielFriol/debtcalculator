import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { first } from 'rxjs/operators';
import { LoginModel, SendVerificationCodeModel, ChangePasswordModel } from '../../shared/models/login.models';
import { environment } from '../../../environments/environment';
import { DebtAdd, DebtConfig } from 'src/app/shared/models/debt.model';

@Injectable()
export class DebtService {

  constructor(
    private http: HttpClient
  ) { }

  getAll(skip, take) {
    return this.http.get(environment.apiUrl + `Debt/${skip}/${take}`).pipe(first()).toPromise();
  }

  getAllByClient(skip, take) {
    return this.http.get(environment.apiUrl + `Debt/GetAllByClient/${skip}/${take}`).pipe(first()).toPromise();
  }

  get(debtId) {
    return this.http.get(environment.apiUrl + `Debt/GetAllByClient/${debtId}`).pipe(first()).toPromise();
  }

  getInfo(debtId) {
    return this.http.get(environment.apiUrl + `Debt/GetDebtInfo/${debtId}`).pipe(first()).toPromise();
  }

  addDebt(request: DebtAdd) {
    return this.http.post(environment.apiUrl + `Debt`, request).pipe(first()).toPromise();
  }

  updateConfig(request: DebtConfig) {
    return this.http.put(environment.apiUrl + `Debt`, request).pipe(first()).toPromise();
  }

  finalize(id) {
    return this.http.delete(environment.apiUrl + `Debt/${id}`).pipe(first()).toPromise();
  }
}

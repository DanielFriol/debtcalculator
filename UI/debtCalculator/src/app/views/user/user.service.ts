import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class UserService {

  constructor(
    private http: HttpClient
  ) { }


  getUsersPaginated(skip, take) {
    return this.http.get(environment.apiUrl + `User/${skip}/${take}`).pipe(first()).toPromise();
  }
  transformInAdmin(request) {
    return this.http.post(environment.apiUrl + `User/Admin`, request).pipe(first()).toPromise();
  }
}

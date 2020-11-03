import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as jwt_decode from 'jwt-decode';
import * as CryptoJS from 'crypto-js';
import { environment } from '../../../environments/environment';
import { StorageDataModel } from '../models/storage.model';
import { UserLoginModel } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class StorageService {

  encryptSecretKey: string = 'rV|"x@K7!(]J=#EmRD!Lupmi7#<]qVzbtDXQ]BLx"-m@LhXuf_R"&j($(_ecO*`';

  constructor(private http: HttpClient) { }

  encryptData(data) {
    try {
      return CryptoJS.AES.encrypt(JSON.stringify(data), this.encryptSecretKey.toString()).toString();
    } catch (e) {
      console.log(e);
    }
  }

  decryptData(data) {
    try {
      if (data) {
        const bytes = CryptoJS.AES.decrypt(data, this.encryptSecretKey.toString());
        if (bytes.toString()) {
          return JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
        }
        return data;
      }
      return null;
    } catch (e) {
      console.log(e);
    }
  }

  getStorage(): StorageDataModel {
    var storageData: StorageDataModel = {
      token: this.decryptData(localStorage.getItem(environment.keys.token)),
      // refreshToken: this.decryptData(localStorage.getItem(environment.keys.refresh_token)),
      expires: new Date(this.decryptData(localStorage.getItem(environment.keys.expires))),
      authTime: new Date(this.decryptData(localStorage.getItem(environment.keys.authTime))),
      user: this.decryptData(localStorage.getItem(environment.keys.user)) as UserLoginModel,
      env: this.decryptData(localStorage.getItem(environment.keys.env)),
    }
    return storageData;
  }

  setStorage(token: string, env: string) {
    const claims = jwt_decode(token);
    let expires = claims['exp'];
    let auth_time = claims['nbf'];
    let user: UserLoginModel = {
      id: claims['id'], email: claims['email'], name: claims['name'], idProfile: claims['idprofile']
    };

    try {
      this.clearStorage();
      localStorage.setItem(environment.keys.token, this.encryptData(token));
      // localStorage.setItem(environment.keys.refresh_token, this.encryptData(refresh_token));
      localStorage.setItem(environment.keys.expires, this.encryptData(new Date(expires * 1000).toString()));
      localStorage.setItem(environment.keys.authTime, this.encryptData(new Date(auth_time * 1000).toString()));
      localStorage.setItem(environment.keys.user, this.encryptData(user));
      localStorage.setItem(environment.keys.env, this.encryptData(env));
    } catch (e) {
      console.log(e);
    }
  }


  refreshStorage(token: string, refresh_token: string, env: string) {
    const claims = jwt_decode(token);
    let expires = claims['exp'];
    try {
      //REMOVE
      localStorage.removeItem(environment.keys.token);
      localStorage.removeItem(environment.keys.expires);
      localStorage.removeItem(environment.keys.env);
      //SETs
      localStorage.setItem(environment.keys.token, this.encryptData(token));
      localStorage.setItem(environment.keys.expires, this.encryptData(new Date(expires * 1000).toString()));
      localStorage.setItem(environment.keys.env, this.encryptData(env));
    } catch (e) {
      console.log(e);
    }
  }

  setEnvironment(env: string) {
    try {
      localStorage.removeItem(environment.keys.env);
      localStorage.setItem(environment.keys.env, this.encryptData(env));
    } catch (e) {
      console.log(e);
    }
  }

  clearStorage() {
    localStorage.removeItem(environment.keys.token);
    localStorage.removeItem(environment.keys.expires);
    localStorage.removeItem(environment.keys.authTime);
    localStorage.removeItem(environment.keys.user);
    localStorage.removeItem(environment.keys.env);
  }

  getExpiresDate(): Date {
    return new Date(this.decryptData(localStorage.getItem(environment.keys.expires)));
  }

  hasData(): boolean {
    return !!localStorage.getItem(environment.keys.env) || !!localStorage.getItem(environment.keys.token);
  }

}

import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { StorageService } from './storage.service';
import { AppService } from '../../app.service';
@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
    private appService: AppService,
    private router: Router,
    private storageService: StorageService,
  ) { }

  async canActivate() {
    if (!this.storageService.getStorage().token) {
      this.storageService.clearStorage();
      this.router.navigate(['/login']);
      this.appService.showLoading.next(false);
      return false;
    } else if (this.storageService.getExpiresDate() < new Date()) {
      this.storageService.clearStorage();
      this.router.navigate(['/login']);
      this.appService.showLoading.next(false);
      return false;
    }

    return true;
  }

}

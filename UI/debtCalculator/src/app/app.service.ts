import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { StorageService } from './shared/services/storage.service';

@Injectable({ providedIn: 'root' })

export class AppService {

    constructor(
        private storage: StorageService
    ) { }

    lastRoute = new BehaviorSubject<string>(null);
    currentRoute = new BehaviorSubject<string>(null);
    showLoading = new BehaviorSubject<boolean>(null);
    showNav = new BehaviorSubject<boolean>(true);

}

import { Component, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { AppService } from './app.service';
import { StorageService } from './shared/services/storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'debtCalculator';

  constructor(
    private appService: AppService,
    private storage: StorageService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.router.events.subscribe((evt) => {
      if (evt instanceof NavigationStart) {
        this.appService.showLoading.next(true);
        this.appService.lastRoute.next(this.router.url);
      }
      if (evt instanceof NavigationEnd) {
        this.appService.showLoading.next(false);
        this.appService.currentRoute.next(this.router.url);
      }
    });
  }
}

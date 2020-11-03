import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/app.service';
import { UserLoginModel } from 'src/app/shared/models/user.model';
import { StorageService } from 'src/app/shared/services/storage.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  showNav = this.appService.showNav;

  constructor(
    private storage: StorageService,
    private appService: AppService
  ) { }

  ngOnInit(): void {
  }

}

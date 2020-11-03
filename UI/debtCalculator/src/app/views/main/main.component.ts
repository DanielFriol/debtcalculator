import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  showNav = this.appService.showNav;

  constructor(
    private appService: AppService
  ) { }

  ngOnInit(): void {
  }

}

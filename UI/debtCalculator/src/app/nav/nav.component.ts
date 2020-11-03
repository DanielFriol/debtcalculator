import { Component, OnInit, Input } from '@angular/core';
import { NavModel } from '../shared/models/nav.model';
import { faUser, faSignOutAlt, faHome, faGlobeAmericas, faBuilding, faLocationArrow, faBriefcase, faHdd, faArrowDown, faArrowUp, faArrowLeft, faArrowAltCircleDown, faArrowAltCircleLeft, faArchive, faMapSigns, faUsers, faBookmark, faSatelliteDish, faBars, faArrowsAlt, faHistory, faPen, faChartLine, faCalculator } from '@fortawesome/free-solid-svg-icons';
import { StorageService } from '../shared/services/storage.service';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs/operators';
import { isNullOrUndefined } from 'util';
import { UserLoginModel } from '../shared/models/user.model';
import { AppService } from '../app.service';
import { Title } from '@angular/platform-browser';
import { UserProfileEnum } from '../shared/enums/user-profile.enum';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  user: UserLoginModel;
  navItems: NavModel[];
  faUser = faUser;
  faSignOutAlt = faSignOutAlt;
  faHome = faHome;
  faCalculator = faCalculator;
  userProfiles = UserProfileEnum;

  constructor(
    private title: Title,
    private storage: StorageService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private appService: AppService,
  ) { }

  async ngOnInit() {
    this.user = this.storage.getStorage().user;
    await this.initNavItems();
  }


  async initNavItems() {
    this.navItems = [
      {
        name: "Início",
        route: '/home',
        icon: faHome,
        administration: false
      },
      {
        name: "Usuários",
        route: '/users',
        icon: faUser,
        administration: true
      },
      {
        name: "Dívidas",
        route: '/debts',
        icon: faCalculator,
        administration: false
      }
    ];
  }


  logout() {
    this.storage.clearStorage();
    this.router.navigate(['/login']);
  }

  collapse(dropdown: string) {
    this.navItems.find(n => n.route == dropdown).collapse = !this.navItems.find(n => n.route == dropdown).collapse;
  }

  changePassword() {
    this.router.navigate(['/change-password']);
  }

  hasAdminAccess(item: NavModel): boolean {
    if (!item.administration ||
      this.user.idProfile != UserProfileEnum.User
    )
      return true;
    else
      return false;
  }

  isAdmin() {
    if (this.user.idProfile == UserProfileEnum.Admin)
      return true;
    else
      return false;
  }

}

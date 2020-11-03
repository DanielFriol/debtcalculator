import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { UserProfileEnum } from 'src/app/shared/enums/user-profile.enum';
import { UserLoginModel, UserModel, UserPaginationModel } from 'src/app/shared/models/user.model';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { StorageService } from 'src/app/shared/services/storage.service';
import { UserService } from './user.service';

@Component({
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user: UserLoginModel;
  users: UserModel[];
  total: number;

  constructor(
    private storage: StorageService,
    private confirmationService: ConfirmationService,
    private notification: NotificationService,
    private service: UserService,

  ) { }

  async ngOnInit() {
    this.user = this.storage.getStorage().user;
    if (this.isAdmin()) {
      await this.getAllUsers(0, 10);
    }
  }


  async getAllUsers(skip, take) {
    await this.service.getUsersPaginated(skip, take).then((result: UserPaginationModel) => {
      this.users = result.data;
      this.total = result.total;
    }).catch(async error => {
      console.log(error);
      this.notification.error("Erro", "Ocorrou um erro inesperado");
    });
  }

  async makeAdmin(id) {
    this.confirmationService.confirm({
      message: 'Você tem certeza que deseja realizar essa operação?',
      accept: async () => {
        var request = {
          id
        };
        await this.service.transformInAdmin(request).then(async x => {
          this.notification.success("Sucesso", "Usuário atualizado com sucesso!");
          await this.getAllUsers(0, 10);
        }).catch(async error => {
          console.log(error);
          this.notification.error("Erro", "Ocorrou um erro inesperado");
        });
      }
    });
  }

  isAdmin() {
    if (this.user.idProfile == UserProfileEnum.Admin)
      return true;
    else
      return false;
  }
}

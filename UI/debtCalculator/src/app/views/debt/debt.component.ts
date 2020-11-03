import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { faCheck, faCog, faCross, faEye, faMinusCircle, faPlus, faRedo, faTimes } from '@fortawesome/free-solid-svg-icons';
import { ConfirmationService } from 'primeng/api';
import { UserProfileEnum } from 'src/app/shared/enums/user-profile.enum';
import { DebtAdd, DebtConfig, DebtInfo, DebtList, DebtResponse } from 'src/app/shared/models/debt.model';
import { UserLoginModel } from 'src/app/shared/models/user.model';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { StorageService } from 'src/app/shared/services/storage.service';
import { DebtService } from './debt.service';

@Component({
  templateUrl: './debt.component.html',
  styleUrls: ['./debt.component.scss']
})
export class DebtComponent implements OnInit {
  debts: DebtList[] = [];
  total: number;
  user: UserLoginModel;
  editDialog: boolean = false;
  addDialog: boolean = false;
  viewDialog: boolean = false;
  faCog = faCog;
  faEye = faEye;
  faMinusCircle = faMinusCircle;
  faPlus = faPlus;
  faCheck = faCheck;
  faRedo = faRedo;
  faTimes = faTimes;
  editConfigForm: FormGroup;
  addDebtForm: FormGroup;
  debt: DebtList;
  debtInfo: DebtInfo = new DebtInfo();

  constructor(
    private storage: StorageService,
    private confirmationService: ConfirmationService,
    private formBuilder: FormBuilder,
    private notification: NotificationService,
    private service: DebtService,
  ) { }

  async ngOnInit() {
    this.user = this.storage.getStorage().user;
    this.editConfigFormSetup();
    this.addDebtFormSetup();
    if (this.isAdmin())
      await this.getAll(0, 10);
    else
      await this.getAllByClient(0, 10);
  }

  showAddDialog() {
    this.addDebtFormSetup();
    this.addDialog = true;
  }


  showEditDialog(id: number) {
    this.editConfigFormSetupValue(id);
    this.editDialog = true;
  }

  editConfigFormSetupValue(id: number) {
    var debt = this.debts.find(x => x.id == id);
    this.debt = debt;
    this.editConfigForm = this.formBuilder.group({
      maxSplit: [debt.maxSplit, [Validators.required, Validators.min(1)]],
      interestType: [debt.interestType, Validators.required],
      interest: [debt.interest, [Validators.required, Validators.min(0.1)]],
      paschoalottoPercentage: [debt.paschoalottoPercentage, [Validators.required, Validators.min(0.1)]]
    })
  }

  editConfigFormSetup() {
    this.editConfigForm = this.formBuilder.group({
      maxSplit: ['', Validators.required],
      interestType: ['', Validators.required],
      interest: ['', Validators.required],
      paschoalottoPercentage: ['', Validators.required]
    })
  }

  addDebtFormSetup() {
    this.addDebtForm = this.formBuilder.group({
      clientCPF: ['', [Validators.required, Validators.minLength(11)]],
      value: ['', [Validators.required, Validators.min(0)]],
      dueDate: ['', Validators.required],
      contactPhone: ['', Validators.required],
      maxSplit: ['', Validators.required],
      interestType: ['', Validators.required],
      interest: ['', Validators.required],
      paschoalottoPercentage: ['', Validators.required]
    })
  }


  async get(skip, take) {
    if (this.isAdmin())
      await this.getAll(skip, take);
    else
      await this.getAllByClient(skip, take);
  }

  async getAll(skip, take) {
    await this.service.getAll(skip, take).then((result: DebtResponse) => {
      this.debts = result.data;
      this.total = result.total;
    }).catch(async error => {
      console.log(error);
      this.notification.error("Erro", "Ocorrou um erro inesperado");
    });
  }


  async getAllByClient(skip, take) {
    await this.service.getAllByClient(skip, take).then((result: DebtResponse) => {
      this.debts = result.data;
      this.total = result.total;
    }).catch(async error => {
      console.log(error);
      this.notification.error("Erro", "Ocorrou um erro inesperado");
    });
  }

  isAdmin() {
    if (this.user.idProfile == UserProfileEnum.Admin)
      return true;
    else
      return false;
  }


  async saveConfig() {
    var form = this.editConfigForm.getRawValue();
    var request: DebtConfig = {
      id: this.debt.id,
      interest: form["interest"],
      interestType: form["interestType"],
      maxSplit: form["maxSplit"],
      paschoalottoPercentage: form["paschoalottoPercentage"]
    }
    await this.service.updateConfig(request).then(async x => {
      this.editDialog = false;
      await this.getAll(0, 10);
      this.notification.success("Sucesso", "Configurações editadas com sucesso!");
    }).catch(async error => {
      console.log(error);
      this.notification.error("Erro", "Ocorrou um erro inesperado");
    });
  }

  viewInfo(id: number) {
    this.debtInfo = new DebtInfo();
    this.service.getInfo(id).then((response: any) => {
      this.debtInfo = response.result;
      this.viewDialog = true;
    }).catch(async error => {
      console.log(error);
      this.notification.error("Erro", "Ocorrou um erro inesperado");
    });
  }

  addThousandPoint(num: number) {
    if (!num)
      return num;
    var lnum = num.toLocaleString('pt-BR');
    return lnum;
  }

  finalize(id: number) {
    this.confirmationService.confirm({
      message: 'Você tem certeza que deseja finalizar essa dívida?',
      accept: async () => {
        await this.service.finalize(id).then(async x => {
          this.notification.success("Sucesso", "Dívida finalizada com sucesso!");
          await this.getAll(0, 10);
        }).catch(async error => {
          console.log(error);
          this.notification.error("Erro", "Ocorrou um erro inesperado");
        });
      }
    })
  }

  async addDebt() {
    var form = this.addDebtForm.getRawValue();
    var request: DebtAdd = {
      clientCPF: form['clientCPF'],
      value: form['value'],
      dueDate: form['dueDate'],
      contactPhone: form['contactPhone'],
      interest: form['interest'],
      interestType: form['interestType'],
      maxSplit: form['maxSplit'],
      paschoalottoPercentage: form['paschoalottoPercentage']
    };
    await this.service.addDebt(request).then(async x => {
      this.notification.success("Sucesso", "A dívida foi adicionado com sucesso");
      await this.getAll(0, 10);
      this.addDialog = false;
    }).catch(async error => {
      console.log(error);
      this.notification.error("Erro", "Ocorrou um erro inesperado");
    });
  }
}

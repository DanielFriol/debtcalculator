import { Component, OnInit } from '@angular/core';
import { StorageService } from 'src/app/shared/services/storage.service';
import { UserLoginModel } from 'src/app/shared/models/user.model';
import { EmailValidator } from 'src/app/shared/validators/email-validator';
import { ChangePasswordModel } from 'src/app/shared/models/login.models';
import { ChangePasswordService } from './change-password.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { Router } from '@angular/router';
import { faSave, faWindowClose } from '@fortawesome/free-solid-svg-icons';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  changePasswordForm: FormGroup;
  user: UserLoginModel;
  faSave = faSave;
  faWindowClose = faWindowClose;

  constructor(
    private fb: FormBuilder,
    private storage: StorageService,
    private service: ChangePasswordService,
    private notification: NotificationService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.user = this.storage.getStorage().user;
    this.changePasswordFormSetup();
  }

  changePasswordFormSetup() {
    this.changePasswordForm = this.fb.group({
      email: [{ value: this.user.email, disabled: true }, [Validators.required, EmailValidator.emailValidator]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmNewPassword: ['', [Validators.required, Validators.minLength(6)]]
    }, { validators: this.passwordConfirming });
  }

  passwordConfirming(c: AbstractControl): { invalid: boolean } {
    if (c.get('newPassword').value !== c.get('confirmNewPassword').value) {
      return { invalid: true };
    }
  }


  async changePassword() {
    var form = this.changePasswordForm.getRawValue();
    var request: ChangePasswordModel = {
      email: this.user.email,
      oldPassword: form['password'],
      newPassword: form['confirmNewPassword'],
      grantType: 'new_password'
    };
    await this.service.changePassword(request).then(async result => {
      this.notification.success("Sucesso", "Sua senha foi alterada com sucesso!");
      this.router.navigate(['/home']);
    }).catch(async error => {
      console.log(error);
      this.notification.error("Erro", "Ocorrou um erro inesperado");
    });
  }

}

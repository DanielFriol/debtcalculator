import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { LoginModel, LoginResult } from 'src/app/shared/models/login.models';
import { UserModel } from 'src/app/shared/models/user.model';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { StorageService } from 'src/app/shared/services/storage.service';
import { EmailValidator } from 'src/app/shared/validators/email-validator';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  registerForm: FormGroup;

  constructor(
    private service: LoginService,
    private appService: AppService,
    private fb: FormBuilder,
    private storage: StorageService,
    private notification: NotificationService,
    private router: Router
  ) { }

  ngOnInit() {
    this.registerFormSetup();
  }

  registerFormSetup() {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, EmailValidator.emailValidator]],
      cpf: ['', [Validators.required, Validators.minLength(11)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
    }, { validators: this.passwordConfirming })
  }


  passwordConfirming(c: AbstractControl): { invalid: boolean } {
    if (c.get('password').value !== c.get('confirmPassword').value) {
      return { invalid: true };
    }
  }

  async register() {
    var form = this.registerForm.getRawValue();
    var request = {
      name: form["name"],
      email: form["email"],
      cpf: form["cpf"],
      password: form["password"]
    };
    await this.service.register(request).then(async (response: UserModel) => {
      this.notification.success("Sucesso", "Cadastro realizado com sucesso!");
      var authRequest: LoginModel = {
        email: form["email"],
        password: form["password"]
      }
      await this.autenticar(request);
    }).catch(async error => {
      console.log(error);
      if (error.error && Array.isArray(error.error)) {
        this.notification.error("Erro", "Ocorrou um erro inesperado");
      } else {
        this.notification.error("Erro", "Ocorrou um erro inesperado");
      }
    });
  }

  async autenticar(request) {
    await this.service.authenticate(request).then((result: LoginResult) => {
      this.storage.setStorage(result.access_token, result.environment);
      this.appService.showNav.next(true);
      this.router.navigate(['/home']);
    }).catch(async error => {
      console.log(error);
      if (error.error && Array.isArray(error.error)) {
        this.notification.error("Erro", "Credenciais inválidas");
      } else {
        this.notification.error("Erro", "Ocorrou um erro inesperado");
      }
    });
  }
}

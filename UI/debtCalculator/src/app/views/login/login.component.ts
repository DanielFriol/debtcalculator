import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { LoginResult } from 'src/app/shared/models/login.models';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { StorageService } from 'src/app/shared/services/storage.service';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private service: LoginService,
    private storage: StorageService,
    private appService: AppService,
    private notification: NotificationService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.loginFormSetup();
  }

  loginFormSetup() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', Validators.required]
    });
  }


  async autenticar() {
    var form = this.loginForm.getRawValue();
    var request = {
      email: form["email"],
      password: form["password"]
    }
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

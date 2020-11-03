import { NgModule } from "@angular/core";
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { Interceptor } from './services/interceptor.service';
import { AuthGuard } from './services/auth-guard.service';
import { StorageService } from './services/storage.service';
import { ValidateTokenService } from './services/validate-token.service';
import { LoadingComponent } from './loading/loading.component';
import { NotificationService } from './services/notification.service';
import { NgxMaskModule } from 'ngx-mask';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import {TooltipModule} from 'primeng/tooltip';
import { PaginationComponent } from './services/pagination.service';


export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: Interceptor, multi: true },
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FontAwesomeModule,
    NgbModule,
    ConfirmDialogModule,
    NgxMaskModule.forRoot(),
    NgbDropdownModule,
    DialogModule,
    TooltipModule
  ],
  declarations: [
    LoadingComponent,
    PaginationComponent
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FontAwesomeModule,
    NgxMaskModule,
    NgbDropdownModule,
    LoadingComponent,
    ConfirmDialogModule,
    NgbModule,
    DialogModule,
    PaginationComponent,
    TooltipModule
  ],
  providers: [
    httpInterceptorProviders,
    AuthGuard,
    StorageService,
    NotificationService,
    AuthGuard,
    ConfirmationService,
    DatePipe,
    ValidateTokenService,
  ],
  entryComponents: []
})
export class SharedModule { }

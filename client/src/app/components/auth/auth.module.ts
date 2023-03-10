import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';

import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { SharedModule } from '../../shared/shared.module';
import { RegisterComponent } from './register/register.component';
import { RegisterStepperComponent } from './register-stepper/register-stepper.component';
import { ProgressComponent } from './progress/progress.component';
import { ProgressStepComponent } from './progress/progress-step/progress-step.component';
import { ProgressStepDirective } from './progress/progress-step.directive';
import { ConfirmUserEmailComponent } from './confirm-user-email/confirm-user-email.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    RegisterStepperComponent,
    ProgressComponent,
    ProgressStepComponent,
    ProgressStepDirective,
    ConfirmUserEmailComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    NgbModule,
    CarouselModule,
    SharedModule,
  ],
})
export class AuthModule {}

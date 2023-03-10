import { RegisterComponent } from './register/register.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ConfirmUserEmailComponent } from './confirm-user-email/confirm-user-email.component';


const routes: Routes = [
  {path:'login', component:LoginComponent},
  { path:'register', component:RegisterComponent},
  { path:'userEmailConfirmation/:code', component:ConfirmUserEmailComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }

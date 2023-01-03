import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountsRoutingModule } from './accounts-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { RolesListComponent } from './roles-list/roles-list.component';
import { CreateRoleComponent } from './create-role/create-role.component';



@NgModule({
  declarations: [
    RolesListComponent,
    CreateRoleComponent
  ],
  imports: [
    CommonModule,
    AccountsRoutingModule,
    SharedModule,
    NgbModule,
    ReactiveFormsModule,
    NgxDatatableModule
  ]
})
export class AccountsModule { }

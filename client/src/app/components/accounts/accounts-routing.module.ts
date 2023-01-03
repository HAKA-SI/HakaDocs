import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { RolesListComponent } from './roles-list/roles-list.component';
import { CreateRoleComponent } from './create-role/create-role.component';



const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'roles',
        component: RolesListComponent,
        data: {
          title: "Roles list",
          breadcrumb: "Roles list"
        }
      }
      ,
      {
        path: 'create-role',
        component: CreateRoleComponent,
        data: {
          title: "Create Role",
          breadcrumb: "Create Role"
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountsRoutingModule { }

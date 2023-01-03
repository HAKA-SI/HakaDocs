import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListCustomerComponent } from './list-customer/list-customer.component';
import { CreateCustomerComponent } from './create-customer/create-customer.component';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-customer',
        component: ListCustomerComponent,
        data: {
          title: "Customer List",
          breadcrumb: "Customer List"
        }
      },
      {
        path: 'create-customer',
        component: CreateCustomerComponent,
        data: {
          title: "Create Customer",
          breadcrumb: "Create Customer"
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomersRoutingModule { }

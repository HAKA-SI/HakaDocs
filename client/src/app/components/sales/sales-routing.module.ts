import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrdersComponent } from './orders/orders.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { AddSaleComponent } from './add-sale/add-sale.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'add-sale',component: AddSaleComponent, data: {title: "Add Sale", breadcrumb: "Add Sale"} },
      { path: 'orders',component: OrdersComponent, data: {title: "Orders", breadcrumb: "Orders"} },
      { path: 'transactions',component: TransactionsComponent,data: { title: "Transactions",breadcrumb: "Transactions" }}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalesRoutingModule { }

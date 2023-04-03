import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { SalesRoutingModule } from './sales-routing.module';
import { OrdersComponent } from './orders/orders.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { AddSaleComponent } from './add-sale/add-sale.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [OrdersComponent, TransactionsComponent, AddSaleComponent],
  imports: [
    CommonModule,
    SalesRoutingModule,
    Ng2SmartTableModule,
    SharedModule,
    NgxDatatableModule
  ]
})
export class SalesModule { }

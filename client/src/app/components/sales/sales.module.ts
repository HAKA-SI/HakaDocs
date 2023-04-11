import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { SalesRoutingModule } from './sales-routing.module';
import { OrdersComponent } from './orders/orders.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { AddSaleComponent } from './add-sale/add-sale.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SelectCustomerComponent } from './add-sale/select-customer/select-customer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectProductsComponent } from './select-products/select-products.component';
import { SetProductNoSnModalComponent } from './add-sale/set-product-no-sn-modal/set-product-no-sn-modal.component';
import { SetProductWithSnModalComponent } from './add-sale/set-product-with-sn-modal/set-product-with-sn-modal.component';
import { SaleSummaryComponent } from './sale-summary/sale-summary.component';
import { SaleOtherDetailsComponent } from './add-sale/sale-other-details/sale-other-details.component';

@NgModule({
  declarations: [OrdersComponent, TransactionsComponent, AddSaleComponent, SelectCustomerComponent, SelectProductsComponent, SetProductNoSnModalComponent, SetProductWithSnModalComponent, SaleSummaryComponent, SaleOtherDetailsComponent],
  imports: [
    CommonModule,
    
    SalesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SmartTableModule,
    SharedModule,
    NgxDatatableModule
  ]
})
export class SalesModule { }

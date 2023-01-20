import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StockEntryComponent } from './physical/stock-entry/stock-entry.component';
import { Routes, RouterModule } from '@angular/router';
import { StockStatusComponent } from './physical/stock-status/stock-status.component';
import { StockMovementsComponent } from './physical/stock-movements/stock-movements.component';



const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'physical/stock-entry', component: StockEntryComponent,data: { title: "Stock Entry",breadcrumb: "Stock Entry"}},
      { path: 'physical/stock-status', component: StockStatusComponent,data: { title: "Stock Status",breadcrumb: "Stock Status"}},
      { path: 'physical/stock-movements', component: StockMovementsComponent,data: { title: "Recorded movements",breadcrumb: "Recorded movements"}},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockRoutingModule { }

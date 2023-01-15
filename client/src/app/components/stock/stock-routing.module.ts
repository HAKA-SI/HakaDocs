import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StockEntryComponent } from './physical/stock-entry/stock-entry.component';
import { Routes, RouterModule } from '@angular/router';



const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'physical/stock-entry',
        component: StockEntryComponent,
        data: {
          title: "Stock Entry",
          breadcrumb: "Stock Entry"
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockRoutingModule { }

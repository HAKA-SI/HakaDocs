import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StockRoutingModule } from './stock-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { StockEntryComponent } from './physical/stock-entry/stock-entry.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { CKEditorModule } from 'ngx-ckeditor';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { StockEntryNoSnComponent } from './stock-entry-no-sn/stock-entry-no-sn.component';
import { StockEntrySnComponent } from './stock-entry-sn/stock-entry-sn.component';
import { StockStatusComponent } from './physical/stock-status/stock-status.component';
import { StockMovementsComponent } from './physical/stock-movements/stock-movements.component';



@NgModule({
  declarations: [
    StockEntryComponent,
    StockEntryNoSnComponent,
    StockEntrySnComponent,
    StockStatusComponent,
    StockMovementsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
    CKEditorModule,
    StockRoutingModule,
    Ng2SmartTableModule,
    NgbModule,
    SharedModule,
    DropzoneModule,
  ]
})
export class StockModule { }

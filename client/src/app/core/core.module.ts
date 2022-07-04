import { SharedModule } from './../shared/shared.module';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { NavBarComponent } from './nav-bar/nav-bar.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { SectionHeaderComponent } from './section-header/section-header.component';



@NgModule({
  declarations: [


    NavBarComponent,
        NotFoundComponent,
        ServerErrorComponent,
        ConfirmDialogComponent,
        SectionHeaderComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ]
  // exports: [
  //   NavBarComponent,
  //   SectionHeaderComponent
  // ]
})
export class CoreModule { }

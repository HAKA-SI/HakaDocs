import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ToastrModule } from 'ngx-toastr';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {NgxPaginationModule} from 'ngx-pagination'; 
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { CdkStepperModule } from '@angular/cdk/stepper';


import { FeatherIconsComponent } from './components/feather-icons/feather-icons.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { ToggleFullscreenDirective } from './directives/fullscreen.directive';
import { ContentLayoutComponent } from './layout/content-layout/content-layout.component';
import { TextInputComponent } from './components/text-input/text-input.component';
import { NavService } from './service/nav.service';
import { WINDOW_PROVIDERS } from './service/windows.service';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { RightSidebarComponent } from './components/right-sidebar/right-sidebar.component';
import { DateInputComponent } from './components/date-input/date-input.component';
import { StepperComponent } from './components/stepper/stepper.component';

const maskConfig: Partial<IConfig> = {
  validation: false,
};
@NgModule({
  declarations: [
    ToggleFullscreenDirective,
    FeatherIconsComponent,
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    ContentLayoutComponent,
    BreadcrumbComponent,
    RightSidebarComponent,
    TextInputComponent,
    DateInputComponent,
    StepperComponent
  ],
  imports: [
    CommonModule,
    NgxPaginationModule,
     NgbModule,
     CdkStepperModule,
    ModalModule.forRoot(),
    Ng2SearchPipeModule,
    BsDatepickerModule.forRoot(),
     BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    TranslateModule,
    RouterModule,
    NgxMaskModule.forRoot(maskConfig),
  ],
  providers: [NavService, WINDOW_PROVIDERS],
  exports: [
    FeatherIconsComponent,
    ToggleFullscreenDirective,
    BsDropdownModule,
    NgxPaginationModule,
    BsDatepickerModule,
    CdkStepperModule,
    Ng2SearchPipeModule,

    ModalModule,
    NgxMaskModule,
    NgbModule,
    ToastrModule,
    TranslateModule,
    TextInputComponent,
    DateInputComponent,
    StepperComponent
  ],
})
export class SharedModule {}

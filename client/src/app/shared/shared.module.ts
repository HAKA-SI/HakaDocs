import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

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
import { ToastrModule } from 'ngx-toastr';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { TranslateModule } from '@ngx-translate/core';
import {  HttpClient} from '@angular/common/http';
import { DateInputComponent } from './components/date-input/date-input.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ButtonViewComponent } from './components/button-view/button-view.component';

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
    ButtonViewComponent,
  ],
  imports: [
    CommonModule,
    NgbModule,
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
    NgxMaskModule,
    NgbModule,
    ToastrModule,
    TranslateModule,
    TextInputComponent,
    DateInputComponent,
    ButtonViewComponent,
  ],
})
export class SharedModule {}

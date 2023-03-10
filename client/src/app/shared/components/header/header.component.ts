import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from '../../../_services/auth.service';
import { User } from 'src/app/_models/user.model';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NavService } from '../../service/nav.service';
import { LanguageService } from 'src/app/core/services/language.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public right_sidebar: boolean = false;
  public open: boolean = false;
  public openNav: boolean = false;
  public isOpenMobile : boolean;
  connectedUser : User;
  currLang: any;
  languages = [];
  langStoreValue: string;

  @Output() rightSidebarEvent = new EventEmitter<boolean>();

  constructor(public navServices: NavService, private authService: AuthService,
    toastr: ToastrService,private languageService: LanguageService) {
    this.authService.currentUser$.subscribe((user) => this.connectedUser=user);
    this.languageService.currentLanguage.subscribe((lang) => this.langStoreValue=lang);

  }

  collapseSidebar() {
    this.open = !this.open;
    this.navServices.collapseSidebar = !this.navServices.collapseSidebar
  }
  right_side_bar() {
    this.right_sidebar = !this.right_sidebar
    this.rightSidebarEvent.emit(this.right_sidebar)
  }

  openMobileNav() {
    this.openNav = !this.openNav;
  }


  ngOnInit() { 
    this.getSupportedLanguages();
   }

  logout() {
   this.authService.logout();
  }

  setLanguage(language) {
    this.languageService.setLanguage(language);
    this.currLang = this.languageService.getAvailableLanguages().find(l => l.lang == language);;
    this.getList();
  }

  getSupportedLanguages() {
    this.currLang = this.languageService.getAvailableLanguages().find(l => l.lang == this.languageService.userLanguage);
    this.getList();
  }

  
  getList() {
    this.languages = [];
    const list = this.languageService.getAvailableLanguages();
    for (let i = 0; i < list.length; i++) {
      const elt = list[i];
      if (elt.lang.toLowerCase() != this.currLang.lang.toLowerCase()) {
        this.languages = [...this.languages, elt];
      }
    }
  }

}

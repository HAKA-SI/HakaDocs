import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  private keyLanguage = 'userLanguage';
  private _userLanguage = '';

  private language = new BehaviorSubject<string>(null);
  currentLanguage = this.language.asObservable();

  private supportedLanguages = [
    { text: 'English', flag: 'flag-icon flag-icon-gb', lang: 'en' },
    { text: 'Fançais', flag: 'flag-icon flag-icon-fr', lang: 'fr' }

  ];

  constructor(private translate: TranslateService) {
    this.initLanguage();
    translate.use(this._userLanguage);
  }

  initLanguage() {
    const value = localStorage.getItem(this.keyLanguage);
    if (value != null) {
      this._userLanguage = value;
    } else {
      const browserLanguage = navigator.language.split('-')[0];
      this._userLanguage = 'en';
      if (this.supportedLanguages.find(l => l.lang == browserLanguage)) {
        this._userLanguage = browserLanguage;
        localStorage.setItem(this.keyLanguage, browserLanguage);
      }
    }

    this.language.next(this._userLanguage);
  }

  setLanguage(language) {
    // console.log('la langue a definir ', language)
    this._userLanguage = language;
    localStorage.setItem(this.keyLanguage, this._userLanguage);
    this.language.next(this._userLanguage);
    this.translate.use(this._userLanguage);
  }

  get userLanguage() {
    return this._userLanguage;
  }

  getAvailableLanguages() {
    return this.supportedLanguages;
  }

  // public languages: string[] = ['en', 'es', 'de'];

  // constructor(public translate: TranslateService) {
  //   let browserLang;
  //   translate.addLangs(this.languages);

  //   if (localStorage.getItem('lang')) {
  //     browserLang = localStorage.getItem('lang');
  //   } else {
  //     browserLang = translate.getBrowserLang();
  //   }
  //   translate.use(browserLang.match(/en|es|de/) ? browserLang : 'en');
  // }

  // public setLanguage(lang) {
  //   this.translate.use(lang);
  //   localStorage.setItem('lang', lang);
  // }
}

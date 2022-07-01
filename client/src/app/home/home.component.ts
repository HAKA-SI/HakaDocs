import {
  AfterViewInit,
  Component,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.ShadowDom,
})
export class HomeComponent implements OnInit {
  loadAPI: Promise<any>;
  themeName='theme-light';

  constructor() {
    // this.loadAPI = new Promise((resolve) => {
    //   this.loadScript();
    //   resolve(true);
    // });
  }
  ngOnInit(): void {
    this.loadAPI = new Promise((resolve) => {
      this.loadScript();
      resolve(true);
    });
  }

  loadScript() {
    var isFound = false;
    var scripts = document.getElementsByTagName('script');
    for (var i = 0; i < scripts.length; ++i) {
      if (
        scripts[i].getAttribute('src') != null
      ) {
        isFound = true;
      }
    }

    if (!isFound) {
      var dynamicScripts = [
        '../../assets/js/jquery.min.js',
        '../../assets/js/bootstrap.bundle.min.js',
        '../../assets/js/jquery.meanmenu.js',
        '../../assets/js/jquery.appear.min.js',
        '../../assets/js/odometer.min.js',
        '../../assets/js/owl.carousel.min.js',
        '../../assets/js/jquery.magnific-popup.min.js',
        '../../assets/js/jquery.nice-select.min.js',
        '../../assets/js/jquery.ajaxchimp.min.js',
        '../../assets/js/form-validator.min.js',
        '../../assets/js/contact-form-script.js',
        '../../assets/js/wow.min.js'
      ];

      for (var i = 0; i < dynamicScripts.length; i++) {
        // let node = document.createElement('script');
        // node.src = dynamicScripts[i];
        // node.type = 'text/javascript';
        // node.async = false;
        // node.charset = 'utf-8';
        // document.getElementsByTagName('body')[0].appendChild(node);
        var url = dynamicScripts[i];
        this.loadScriptfunction(url);
      }
    }
  }

  public loadScriptfunction(url: string) {
    const body = <HTMLDivElement>document.body;
    const script = document.createElement('script');
    script.innerHTML = '';
    script.src = url;
    script.async = false;
    script.defer = true;
    body.appendChild(script);
  }

  // if (localStorage.getItem('spix_theme') === 'theme-dark') {

	// 	setTheme('theme-dark');
	// 	document.getElementById('slider').checked = false;
	// } else {
	// 	setTheme('theme-light');
	//   document.getElementById('slider').checked = true;
	// }

  // function setTheme(themeName) {
  //   localStorage.setItem('spix_theme', themeName);
  //   document.documentElement.className = themeName;
  // }
  changeTheme(){
    if(this.themeName==='theme-dark')
    {
      this.themeName='theme-light';
    } else {
      this.themeName='theme-light';
    }
  }
}

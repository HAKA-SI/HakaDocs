import { ConfirmService } from './../services/confirm.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/components/auth/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements AfterViewInit{
  constructor(public accountService: AuthService,private confirmService: ConfirmService,
     private toastr: ToastrService, private router: Router) {
     //  this.accountService.clientsList$.subscribe((clients) => this.clients=clients);
      }
  ngAfterViewInit(): void {
    setTimeout(() => {
     //this.dropdown.hide();
    }, 3);
  }

  logout() {
    this.confirmService.confirm('confirmation de deconnexion','voulez-voulez vraiment vous déconnecter ?')
    .subscribe(result => {
      if(result) {
        this.accountService.logout();
        this.toastr.info('vous êtes déconnecté');
        this.router.navigateByUrl('/account/login');
      }
    })

  }

}

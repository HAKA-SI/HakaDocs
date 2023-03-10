import { AuthService } from '../../_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}
  canActivate():boolean {
    if(this.authService.isLogin()) return true;
    this.router.navigateByUrl('auth/login');
    return false;
    // return this.accountService.currentUser$.pipe(
    //   map((user) => {
    //     if (user) return true;
    //     this.toastr.error('You shall not pass!');
    //   })
    // );
  }
}

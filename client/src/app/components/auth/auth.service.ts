import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject } from 'rxjs';
import { User } from 'src/app/shared/models/user.model';
import { environment } from 'src/environments/environment';
import {  tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl;
  private currentuserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentuserSource.asObservable();
  jwtHelper = new JwtHelperService();
  redirectUrl: string;
  decodedToken: any;

  constructor(
    private http: HttpClient,
    //private presenceService: PresenceService,
    private router: Router
  ) {}

  login(model: any) {
    const url = this.baseUrl + "auth/login";
    return this.http.post<User>(url, model).pipe(
      tap((user: User) => {
       // let user = data.user;
       // let clients = data.clients;
        this.setCurrentuser(user);
      })
    );
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

  isLogin(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    const isExpired= !this.jwtHelper.isTokenExpired(token);
    return isExpired;
  }


  logout() {
    localStorage.removeItem('user');
  //  localStorage.removeItem('clients');
    this.currentuserSource.next(null);
    // this.presenceService.stopHubConnection();
    // this.dashboardService.stopHubConnection();
    this.router.navigate(['/auth/login']);
    this.decodedToken=null;
  }

  setCurrentuser(user: User) {
    user.roles = [];
    const roles = this.getDecodeToken(user.token).role;
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    localStorage.setItem('token', JSON.stringify(user.token ));
    this.currentuserSource.next(user);
  }



  getDecodeToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  // isLogin(): boolean {
  //   var response: boolean;
  //   if (
  //     this.currentUser$.subscribe((user) => {
  //       if (user) return (response = true);
  //       return (response = false);
  //     })
  //   )
  //     return response;
  // }


  register(values: any) {
    return this.http.post(this.baseUrl + 'auth/register', values);
  }


  checkEmailExists(email: string) {
    return this.http.get(this.baseUrl+'auth/emailexists?email='+email);
  }
}

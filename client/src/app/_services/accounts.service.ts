import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  private baseUrl = environment.apiUrl + 'account/';


  constructor(private http: HttpClient, private router: Router) { }


  getRoleList(haKaDocClientId: number) {
    return this.http.get(this.baseUrl + 'GetRoleList/' + haKaDocClientId);
  }

  createRole(haKaDocClientId: number, roleName: string) {
    return this.http.post(this.baseUrl + 'CreateRole/' + haKaDocClientId + '/' + roleName, {});
  }

  createAccount(clientId, user: any) {
    return this.http.post(this.baseUrl + 'CreateAccount/' + clientId, user);
  }

  checkEmailExists(clientId: number, email: string): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + email + '/emailexists/' + clientId);
  }
  userNameExist(userName: string) {
    return this.http.get(this.baseUrl + userName + '/VerifyUserName');
  }

  confirmEmail(token: string) {
    return this.http.get(this.baseUrl + 'emailValidation/' + token);
  }




}

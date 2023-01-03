import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl = environment.apiUrl+'Customer/';
  
  constructor(private http: HttpClient, private router: Router) {}

  createCustomer(customer,clientId){
    return this.http.post(this.baseUrl+"CreateCustomer/"+clientId,customer);
  }
}

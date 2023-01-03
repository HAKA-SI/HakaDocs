import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private baseUrl = environment.apiUrl+'common/';
  public genders =[
    {name:'Homme', id:1},
    {name:'Femme', id:0}
  ]

  
  constructor(private http: HttpClient, private router: Router) {}
  getCities(){
    return this.http.get(this.baseUrl+  'GetCities');
  }
}

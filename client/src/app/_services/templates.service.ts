import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TemplatesService {
  private baseUrl = environment.apiUrl+'Customer/';

  constructor(private http: HttpClient) { }

  getInvoiceTemplates(clientId) {
    return this.http.get(this.baseUrl+'GetInvoiceTemplates/'+clientId);
  }
}

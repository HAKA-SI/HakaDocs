import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Store } from 'src/app/_models/store.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StoresService {

  private baseUrl = environment.apiUrl + 'Stores/';
  private currentuserStore = new ReplaySubject<Store>(1);
  currentStore$ = this.currentuserStore.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  storeList(clientId: number) {
    return this.http.get(this.baseUrl + "StoreList/" + clientId).pipe(
      tap((stores: Store[]) => {
        // let user = data.user;
        // let clients = data.clients;
        if (stores.length === 1) {
           this.currentuserStore.next(stores[0]);
        } else{
          this.currentuserStore.next(null);
        }
      })
    );
    ;
  }

  storeStock(storeId:number){
    return this.http.get(this.baseUrl+'StoreStock/'+storeId);
  }
}

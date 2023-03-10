import { Component,OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user.model';
import { Customer } from 'src/app/_models/customer.model';
import { AuthService } from '../../../_services/auth.service';
import { CustomerService } from '../customer.service';
import {TranslateService} from "@ngx-translate/core";
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
@Component({
  selector: 'app-list-customer',
  templateUrl: './list-customer.component.html',
  styleUrls: ['./list-customer.component.scss'],
  animations: [SharedAnimations]
})


export class ListCustomerComponent implements OnInit {
  page: number = 1;
  public products = [];
 searchText:string;
  loggedUser: User;
  customers:any[]=[];

  constructor(private customerService:CustomerService,private authService: AuthService,  private translationService: TranslateService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
   }
  
  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers() {
   this.customerService.getCustomerList(this.loggedUser.haKaDocClientId).subscribe((response:Customer[]) => {
    if (response.length>0) {  
      this.customers = response;
      this.customers.forEach(element => {
        element.editLink='/customers/edit-customer/'+element.id
      });
    }
    })
  }

}

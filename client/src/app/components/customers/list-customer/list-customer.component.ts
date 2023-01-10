import { Component,OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user.model';
import { Customer } from 'src/app/_models/customer.model';
import { AuthService } from '../../auth/auth.service';
import { CustomerService } from '../customer.service';
import {TranslateService} from "@ngx-translate/core";
import { ButtonViewComponent } from 'src/app/shared/components/button-view/button-view.component';
@Component({
  selector: 'app-list-customer',
  templateUrl: './list-customer.component.html',
  styleUrls: ['./list-customer.component.scss']
})


export class ListCustomerComponent implements OnInit {
  // customers:Customer[]=[];
  customers:any[]=[];
  loggedUser: User;
  public settings: any;
  
  columnheaders: string[];


  constructor(private customerService:CustomerService,private authService: AuthService,  private translationService: TranslateService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
   }

   setColumnheaders(): void {
    //USER.FIRST_NAME,... are in the i18n file
    let firstName = 'create-customer.firstName';
    let lastName = 'create-customer.lastName';
    let email = 'create-customer.email';
    let phoneNumber = 'create-customer.phoneNumber';
    let secondPhoneNumber = 'create-customer.secondPhoneNumber';
    let city = 'create-customer.city';
    this.columnheaders = ['','','']
   //Used TranslateService from @ngx-translate/core
    this.translationService.get(firstName).subscribe(label => this.columnheaders[0] = label);
    this.translationService.get(lastName).subscribe(label => this.columnheaders[1] = label);
    this.translationService.get(email).subscribe(label => this.columnheaders[2] = label);
    this.translationService.get(phoneNumber).subscribe(label => this.columnheaders[3] = label);
    this.translationService.get(secondPhoneNumber).subscribe(label => this.columnheaders[4] = label);
    this.translationService.get(city).subscribe(label => {this.columnheaders[5] = label;
      this.loadTableSettings();
    });
  }

  loadTableSettings(): void {
    this.settings = {
      actions: {
        add: false,
        edit: false,
        delete: false,
        // custom: [{ name: 'ourCustomAction', title: '<a href=\'/customers/create-customer/\' class=\'btn btn-success\'>editer </a>' }],
        // position: 'right'
      },
      columns: {
        button: {
          title: 'Button',
          type: 'custom',
          renderComponent: ButtonViewComponent,
          onComponentInitFunction(instance) {
            instance.save.subscribe(row => {
              alert(`${row.id} saved!`)
            });
          }
        },

  
        firstName: {
          title: this.columnheaders[0],
        },
        lastName: {
          title: this.columnheaders[1]
        },
        email: {
          title:this.columnheaders[2]
        },
        phoneNumber: {
          title:this.columnheaders[3]
        },
        secondPhoneNumber: {
          title:this.columnheaders[4]
        },
        city: {
          title:this.columnheaders[5]
        }
       
      },

    };
  }

  
  ngOnInit(): void {
    this.setColumnheaders();
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

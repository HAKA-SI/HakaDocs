import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user.model';
import { CommonService } from 'src/app/_services/common.service';
import { AuthService } from '../../auth/auth.service';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.scss']
})
export class EditCustomerComponent implements OnInit {

  public customerForm: FormGroup;
  cities: any[] = [];
  loggedUser: User;
  customer:any;

  constructor(private authService: AuthService, private formBuilder: FormBuilder, public commonService: CommonService
    , private toastr: ToastrService, private customerService: CustomerService,private route: ActivatedRoute,private router: Router) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.createCustomerForm();     
    this.getCities();
  }


  ngOnInit(): void {

    const  customerId = this.route.snapshot.params['id'];
    if(!!customerId) {
      this.customerService.getCustomerById(customerId,this.loggedUser.haKaDocClientId).subscribe((response) => {
        this.customer =response; 
        this.customerForm.patchValue(this.customer);
        if(!!this.customer.dateOfBirth)
        this.customerForm.get('dateOfBirth').patchValue(this.formatDate(this.customer.dateOfBirth as Date));
      },error => {
        this.toastr.error(error.rror);
        this.router.navigate(['/customers/list-customer']);
      })
    } else {
      this.toastr.error("no customer parameter found");
      this.router.navigate(['/customers/list-customer']);
    }

    // this.createAccountForm();
    // this.getCities();
  }


  createCustomerForm() {
    this.customerForm = this.formBuilder.group({
      lastName: ['', Validators.required],
      firstName: ['', Validators.required],
      customerCode: [''],
      gender: [null],
      dateOfBirth: [],
      cityId: [null, Validators.required],
      phoneNumber: ['', Validators.required],
      secondPhoneNumber: [''],
      email: [
        '',
        [Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
      ],
      cni: [''],
      passport: [''],
      iddoc: [''],
    })
  }


  private formatDate(date) {
    let newDate = new Date(date);
    return newDate.toJSON().split('T')[0];
}


  getCities() {
    this.commonService.getCities().subscribe((response: any[]) => this.cities = response);
  }

  save() {
    this.customerService.updateCustomerData(this.customer.id,this.loggedUser.haKaDocClientId,this.customerForm.value).subscribe(() => {
      this.toastr.success("enregistrement terminé...");
      this.router.navigate(['/customers/list-customer']);
    })
  }


}

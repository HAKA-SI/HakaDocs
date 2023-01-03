import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user.model';
import { CommonService } from 'src/app/_services/common.service';
import { AuthService } from '../../auth/auth.service';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styleUrls: ['./create-customer.component.scss']
})
export class CreateCustomerComponent implements OnInit {
  public customerForm: FormGroup;
  public permissionForm: FormGroup;
  cities:any[]=[];
  loggedUser:User;

  constructor(private authService: AuthService,private formBuilder: FormBuilder, public commonService: CommonService
    , private toastr: ToastrService, private customerService:CustomerService) {
    this.createAccountForm();
    this.getCities();
    this.authService.currentUser$
    .pipe(take(1))
    .subscribe((user) => (this.loggedUser = user));
    // this.createPermissionForm();
  }

  
  createAccountForm() {
    this.customerForm = this.formBuilder.group({
      lastName: ['', Validators.required],
      firstName: ['', Validators.required],
      gender: [null],
      dateOfBirth: [],
      resCityId: [null, Validators.required],
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
  // createPermissionForm() {
  //   this.permissionForm = this.formBuilder.group({
  //   })
  // }

  ngOnInit() {
  }


  getCities() {
    this.commonService.getCities().subscribe((response:any[]) => this.cities=response);
  }


  save() {
    console.log(this.customerForm.value);
    this.customerService.createCustomer(this.customerForm.value,this.loggedUser.haKaDocClientId).subscribe(() => {
      this.toastr.success("enregistrement terminé...");
      this.customerForm.reset();
    })
  }

}

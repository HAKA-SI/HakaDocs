import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { take } from 'rxjs/operators';
import { Customer } from 'src/app/_models/customer.model';
import { User } from 'src/app/_models/user.model';
import { AuthService } from 'src/app/_services/auth.service';
import { CustomerService } from 'src/app/_services/customer.service';
import { ProductsService } from '../../products/products.service';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-sale',
  templateUrl: './add-sale.component.html',
  styleUrls: ['./add-sale.component.scss']
})
export class AddSaleComponent implements OnInit {
  saleForm: FormGroup;
  loggedUser: User;
  customers: Customer[] = [];
  subProducts: SubProduct[] = [];
  physicalProductGroupId = environment.phisicalProductGroupId;


  constructor(private fb: FormBuilder, private authService: AuthService, private customerService: CustomerService,
    private productService: ProductsService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.createSaleForm();
  }

  ngOnInit(): void {
    this.getCustomers();
    this.getSubProducts();

  }

  getSubProducts() {
    this.productService.getSubProducts(this.loggedUser.haKaDocClientId, this.physicalProductGroupId).subscribe((response: SubProduct[]) => this.subProducts = response);
  }

  getCustomers() {
    this.customerService.getCustomerList(this.loggedUser.haKaDocClientId).subscribe((response: any) => this.customers = response);
  }

  createSaleForm() {
    this.saleForm = this.fb.group({
      // addressForm: this.fb.group({
      //   firstName: [null, Validators.required],
      //   lastName: [null, Validators.required],
      //   street: [null, Validators.required],
      //   city: [null, Validators.required],
      //   state: [null, Validators.required],
      //   zipCode: [null, Validators.required],
      // }),
      // deliveryForm: this.fb.group({
      //   deliveryMethod: [null, Validators.required]
      // }),
      // paymentForm: this.fb.group({
      //   nameOnCard: [null, Validators.required]
      // })
    })
  }

}

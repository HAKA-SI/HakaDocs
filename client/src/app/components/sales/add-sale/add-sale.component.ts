import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
import { Customer } from 'src/app/_models/customer.model';
import { User } from 'src/app/_models/user.model';
import { AuthService } from 'src/app/_services/auth.service';
import { CustomerService } from 'src/app/_services/customer.service';
import { ProductsService } from '../../products/products.service';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { OrdersService } from 'src/app/_services/orders.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-sale',
  templateUrl: './add-sale.component.html',
  styleUrls: ['./add-sale.component.scss']
})
export class AddSaleComponent implements OnInit {
  saleDetailsForm: FormGroup;
  loggedUser: User;
  customers: Customer[] = [];
  subProducts: SubProduct[] = [];
  physicalProductGroupId = environment.phisicalProductGroupId;
  basket$: Observable<any>;



  constructor(private fb: FormBuilder, private authService: AuthService, private customerService: CustomerService,
    private productService: ProductsService, private ordersService: OrdersService, private toastr: ToastrService, private router: Router) {

    this.ordersService.basket$.subscribe((basket) => {
      localStorage.setItem('physicalBasket', JSON.stringify(basket));

    });

    // this.setBasketFromLocalStorage();
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.createSaleDetailsForm();
  }


  setBasketFromLocalStorage() {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    this.basket$ = this.ordersService.basket$;
    this.getCustomers();
    this.getSubProducts();

  }

  getSubProducts() {
    this.productService.getSubProducts(this.loggedUser.haKaDocClientId, this.physicalProductGroupId).subscribe((response: SubProduct[]) => this.subProducts = response);
  }

  getCustomers() {
    this.customerService.getCustomerList(this.loggedUser.haKaDocClientId).subscribe((response: any) => this.customers = response);
  }

  createSaleDetailsForm() {
    this.saleDetailsForm = this.fb.group({
      orderDate: [null, Validators.required],
      orderNum: [''],
      observation: [''],
      invoiceSendingType: [null],
      delivered: [null, Validators.required],
      paimentType: [null, Validators.required],
      amountPaid: [null],
    });
  }

  orderSaveCompleted(event) {
    if (!event) {
      this.toastr.error("impossible d'enregitrer la vente");
      return;
    }
    else
      this.toastr.success("enregistrement terminée....");
    this.ordersService.resetBasket();
    this.router.navigateByUrl('/sales/add-sale', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/sales/add-sale']);
    });

  }

}

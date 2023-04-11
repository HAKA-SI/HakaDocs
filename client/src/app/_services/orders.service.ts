import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Customer } from '../_models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  
  

  private basketSource = new BehaviorSubject<any>({});
  basket$ = this.basketSource.asObservable();
  constructor() { }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  setBasketCustomer(customer: Customer) {
    let curentBasketValue: any = this.getCurrentBasketValue();
    curentBasketValue.customer = customer;
    this.basketSource.next(curentBasketValue);
  }

  calculateTotal() {
    const curentBasketValue = this.getCurrentBasketValue();

    const products = curentBasketValue.products;
    if (!!products) {
      products.forEach(element => {
        if (element.withSerialNumber) {
          element.subTotal = element.subProductSNs.reduce((a, b) => (b.quantity * element.unitPrice) + a, 0);
          element.discount = element.subProductSNs.reduce((a, b) => b.discount + a, 0);
          element.quantity = element.subProductSNs.length;
          element.total = element.subTotal - element.discount;
        } else {
          element.subTotal = element.quantity * element.unitPrice;
          element.total = element.subTotal - element.discount;
        }
      });
      curentBasketValue.subTotal = curentBasketValue.products.reduce((a, b) => b.subTotal + a, 0);
      curentBasketValue.total = curentBasketValue.products.reduce((a, b) => b.total + a, 0);
      this.basketSource.next(curentBasketValue);
    }
  }

  addSubProductToBasket(prod) {
    let curentBasketValue: any = this.getCurrentBasketValue();
    if (!!curentBasketValue.products) {
      const idx = curentBasketValue.products.findIndex(a => a.id === prod.id);
      if (idx === -1) curentBasketValue.products.push(prod);
      else curentBasketValue.products[idx] = prod;
      this.basketSource.next(curentBasketValue);
    }
    else {
      curentBasketValue.products = [];
      curentBasketValue.products.push(prod);
      this.basketSource.next(curentBasketValue);

    }
    this.calculateTotal();
  }

  getProductInBasket(id: number) {
    const cbasket = this.getCurrentBasketValue();
    if (cbasket === null || cbasket.products === undefined) return null;
    const idx = cbasket.products.findIndex(a => a.id === id);
    if (idx === -1) return null;

    return cbasket.products[idx];
  }

  removeProductToBasket(id: number) {
    const cbasket = this.getCurrentBasketValue();
    const idx = cbasket.products.findIndex(a => a.id === id);
    if (cbasket.products.length === 1) cbasket.products = [];
    else cbasket.products.splice(idx, 1);
    console.log(cbasket.products);
    this.basketSource.next(cbasket);
    this.calculateTotal();
  }

  setBasketDetails(selectedValue: any) {
    const cbasket = this.getCurrentBasketValue();
    cbasket.details = selectedValue;
    this.basketSource.next(cbasket);
  }

  getBasketDetails(){
    const cbasket = this.getCurrentBasketValue();
    return cbasket.details??null;
  }
  resetBasket() {
    this.basketSource.next({});
  }


}

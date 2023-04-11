import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/_models/customer.model';
import { OrdersService } from 'src/app/_services/orders.service';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';

@Component({
  selector: 'app-select-customer',
  templateUrl: './select-customer.component.html',
  styleUrls: ['./select-customer.component.scss'],
  animations: [SharedAnimations]
})
export class SelectCustomerComponent implements OnInit {
  @Input() customers: Customer[];
  page: number = 1;
  searchText: string;
  constructor(public ordersService: OrdersService) { }

  ngOnInit(): void {
  }

  selectLine(item) {
    this.ordersService.setBasketCustomer(item);
  }

}

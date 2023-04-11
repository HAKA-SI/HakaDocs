import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { OrdersService } from 'src/app/_services/orders.service';

@Component({
  selector: 'app-sale-summary',
  templateUrl: './sale-summary.component.html',
  styleUrls: ['./sale-summary.component.scss']
})
export class SaleSummaryComponent implements OnInit {
  @Input() saleDetailsForm: FormGroup;
  constructor(public ordersService:OrdersService) { }

  ngOnInit(): void {
  }

  save() {
    alert('save functions comes soom');
  }
  reset() {
this.ordersService.resetBasket();
this.saleDetailsForm.reset();
  }
}

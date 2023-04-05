import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/_models/customer.model';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';

@Component({
  selector: 'app-select-customer',
  templateUrl: './select-customer.component.html',
  styleUrls: ['./select-customer.component.scss'],
  animations: [SharedAnimations]
})
export class SelectCustomerComponent implements OnInit {
@Input() customers:any;
page: number = 1;
  searchText:string;
  constructor() { }

  ngOnInit(): void {
  }

  selectLine(item) {
    this.customers.forEach(element => {
      element.selected=false;
    });
   item.selected=true;
  }

}

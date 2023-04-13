import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PhysicalBasket } from 'src/app/_models/physical.basket.model';
import { User } from 'src/app/_models/user.model';
import { OrdersService } from 'src/app/_services/orders.service';
import { ConfirmService } from 'src/app/core/services/confirm.service';

@Component({
  selector: 'app-sale-summary',
  templateUrl: './sale-summary.component.html',
  styleUrls: ['./sale-summary.component.scss']
})
export class SaleSummaryComponent implements OnInit {
  @Input() saleDetailsForm: FormGroup;
  @Input() loggedUser:User;
  constructor(public ordersService: OrdersService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
  }

  save() {
    let basket = this.ordersService.getCurrentBasketValue();
    basket.details.delivered =JSON.parse(basket.details.delivered.toString());
    basket.customerId = basket.customer.id;
    basket.insertUserId = this.loggedUser.id;
    basket.details.paimentType =JSON.parse(basket.details.paimentType.toString());
   for (let index = 0; index < basket.details.invoiceSendingType.length; index++) {
     basket.details.invoiceSendingType[index]=JSON.parse(basket.details.invoiceSendingType[index]);
   }
    console.log(basket);
    
    const msg = "voulez-vous vraiment finaliser cette vente de <h3><b>" + basket.total + "</b></h3> pour le client <h3><b>"
      + basket.customer?.lastName + " " + basket.customer?.lastName + "?</b></h3>";

    this.confirmService.confirm('confirmation ', msg)
    .subscribe(result => {
      if (result) {
        this.ordersService.saveOrder(this.loggedUser.haKaDocClientId, basket).subscribe(() => {
     
          // this.toastr.success("suppression éffectuée....");
          console.log("suppression éffectuée....");
        }, error => {
          // this.toastr.error(error.message);
          console.log(error);
        })
      }
    })
    


  }
  reset() {
    this.ordersService.resetBasket();
    this.saleDetailsForm.reset();
  }
}

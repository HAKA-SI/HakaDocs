import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { OrdersService } from 'src/app/_services/orders.service';
import { ConfirmService } from 'src/app/core/services/confirm.service';

@Component({
  selector: 'app-sale-summary',
  templateUrl: './sale-summary.component.html',
  styleUrls: ['./sale-summary.component.scss']
})
export class SaleSummaryComponent implements OnInit {
  @Input() saleDetailsForm: FormGroup;
  constructor(public ordersService: OrdersService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
  }

  save() {
    this.ordersService.basket$.subscribe((basket) => {
      const msg = "voulez-vous vraiment finaliser cette vente de <b>" + basket.total + "</b> pour le client <b>"
        + basket.customer?.lastName + " " + basket.customer?.lastName + "?</b>";
      this.confirmService.confirm('confirmation ', msg)
      .subscribe(result => {
        if (result) {
          // this.productService.deleteCategory(this.loggedUser.haKaDocClientId, categorie.id).subscribe(() => {
          //   const idx = this.categories.findIndex(a => a.id === categorie.id);
          //   this.categories.splice(idx, 1);
          //   this.toastr.success("suppression éffectuée....");
          // }, error => {
          //   this.toastr.error(error.message);
          // })
        }
      })
    })


  }
  reset() {
    this.ordersService.resetBasket();
    this.saleDetailsForm.reset();
  }
}

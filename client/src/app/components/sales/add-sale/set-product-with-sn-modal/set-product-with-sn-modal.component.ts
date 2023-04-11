import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ProductsService } from 'src/app/components/products/products.service';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';

@Component({
  selector: 'app-set-product-with-sn-modal',
  templateUrl: './set-product-with-sn-modal.component.html',
  styleUrls: ['./set-product-with-sn-modal.component.scss'],
  animations: [SharedAnimations]
})
export class SetProductWithSnModalComponent implements OnInit {
  subProduct: any;
  clientId: any;
  subProductSns = [];
  page: number = 1;
  searchText: string;
  @Output() subProductData: any = new EventEmitter();
  totalAmount = 0;
  totalDiscount = 0;

  constructor(private productsService: ProductsService, public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
    this.getSubProductSns();
  }

  getSubProductSns() {
    this.productsService.subProductSNBySubProductId(this.clientId, this.subProduct.id).subscribe((response: any) => {
      response.forEach(element => {
        if(!!this.subProduct.subProductSNs) {
          const elementFromBasket = this.subProduct.subProductSNs.find(a => a.id ===element.id);
          if(!!elementFromBasket) {
            element.selected=true;
            element.discount = elementFromBasket.discount;
          } else
          element.discount = 0;
        }
        else {
          element.discount = 0;
        }
      });
      this.subProductSns = response;
      this.calculateTotal();
    })
  }

  selectIem(item) {
    const idx = this.subProductSns.findIndex(a => a.id === item.id);
    this.subProductSns[idx].selected = !this.subProductSns[idx].selected;
    this.calculateTotal();
  }


  closeModal() {
    this.bsModalRef.hide();
  }

  save() {
     this.subProduct.subProductSNs = this.subProductSns.filter(a => a.selected);
    this.subProductData.emit(this.subProduct);
    this.bsModalRef.hide();
  }

  calculateTotal() {
    this.totalAmount = 0;
    this.totalDiscount = 0;
    this.subProductSns.forEach(element => {
      if (element.selected) {
        this.totalAmount += this.subProduct.unitPrice;
        if (!!element.discount) this.totalDiscount += element.discount;
      }
    });
  }
}

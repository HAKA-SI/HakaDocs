import { Component, Input, OnInit } from '@angular/core';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
import { SetProductNoSnModalComponent } from '../add-sale/set-product-no-sn-modal/set-product-no-sn-modal.component';
import { SetProductWithSnModalComponent } from '../add-sale/set-product-with-sn-modal/set-product-with-sn-modal.component';
import { OrdersService } from 'src/app/_services/orders.service';
import { ConfirmService } from 'src/app/core/services/confirm.service';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user.model';

@Component({
  selector: 'app-select-products',
  templateUrl: './select-products.component.html',
  styleUrls: ['./select-products.component.scss'],
  animations: [SharedAnimations]
})
export class SelectProductsComponent implements OnInit {
  page: number = 1;
  searchText: string;
  // @Input() loggedUser: User;
  @Input() physicalProductGroupId: number;
  @Input() subProducts: SubProduct[] = [];
  @Input() loggedUser: User;
  bsModalRef: BsModalRef;
  selectedProducts: any[] = [];
  basket$: Observable<any>;
  selectedIds = [];




  constructor(private modalService: BsModalService, public ordersService: OrdersService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
    this.basket$ = this.ordersService.basket$;
    this.basket$.pipe(take(1)).subscribe((basket) => {
      if (!!basket) {
        if (basket.products !== undefined && basket.products !== null) this.selectedIds = basket.products.map(a => a.id);
      }
    });
  }

  selectLine(item: SubProduct) {
    const idx = this.selectedProducts.findIndex(a => a.id === item.id);
    if (idx === -1) {
      if (!item.withSerialNumber)
        this.showModalNoSN(item);
      else
        this.showModalWithSN(item);
    }
  }

  showModalWithSN(subProduct: any) {
    const basketProduct = this.ordersService.getProductInBasket(subProduct.id);
    if(!!basketProduct) subProduct.subProductSNs = basketProduct.subProductSNs;    
    
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        subProduct,
        clientId: this.loggedUser.haKaDocClientId
      },
    };
    this.bsModalRef = this.modalService.show(SetProductWithSnModalComponent, config);
    this.bsModalRef.content.subProductData.subscribe(
      (response: any) => {
        this.ordersService.addSubProductToBasket(response);
        const idx = this.selectedIds.findIndex(a => a.id);
        if (idx === -1) this.selectedIds.push(subProduct.id);
      }
    );
  }
  showModalNoSN(subProduct: any) {
    const basketProduct = this.ordersService.getProductInBasket(subProduct.id);
    if (!!basketProduct) {
      subProduct.newqty = basketProduct.quantity ?? 0;
      subProduct.discount = basketProduct.discount ?? 0;
    } else {
      subProduct.newqty = 0;
      subProduct.discount = 0;
    }
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        subProduct
      },
    };

    this.bsModalRef = this.modalService.show(SetProductNoSnModalComponent, config);
    this.bsModalRef.content.subProductData.subscribe(
      (response: any) => {
        this.ordersService.addSubProductToBasket(response);
        const idx = this.selectedIds.findIndex(a => a.id);
        if (idx === -1) this.selectedIds.push(subProduct.id);
      }
    );
  }

  removeProduct(id) {
    this.confirmService.confirm('confirmation ', 'voulez vous vraiment retirer ce produit ?')
      .subscribe(result => {
        if (result) {
          this.ordersService.removeProductToBasket(id);
          const idx = this.selectedIds.findIndex(a => a.id);
          this.selectedIds.splice(idx, 1);
        }
      })
  }


}

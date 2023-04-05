import { Component, Input, OnInit } from '@angular/core';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
import { SetProductNoSnModalComponent } from '../add-sale/set-product-no-sn-modal/set-product-no-sn-modal.component';
import { SetProductWithSnModalComponent } from '../add-sale/set-product-with-sn-modal/set-product-with-sn-modal.component';

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
  bsModalRef: BsModalRef;
  selectedProducts:any[]=[];


  constructor(private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  selectLine(item:SubProduct) {
   const idx = this.selectedProducts.findIndex(a => a.id ===item.id);
   if(idx===-1){
    if(!item.withSerialNumber)
    this.showModalNoSN('add',item);
    else
    this.showModalWithSN('add',item);
   }
  }

  showModalWithSN(editionMode: 'add' | 'edit', subProduct: any) {
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        subProduct,
        editionMode
      },
    };
    
    this.bsModalRef = this.modalService.show(SetProductWithSnModalComponent, config);
    this.bsModalRef.content.subProductData.subscribe(
      (response: any) => {
        if (editionMode = 'add') {
         // this.products.unshift(product);
        } else {
          // const idx = this.products.findIndex(a => a.id === product.id);
          // this.products[idx] = product;
        }
      }
    );
  }
  showModalNoSN(editionMode: 'add' | 'edit', subProduct: any) {
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        subProduct,
        editionMode
      },
    };
    
    this.bsModalRef = this.modalService.show(SetProductNoSnModalComponent, config);
    this.bsModalRef.content.subProductData.subscribe(
      (response: any) => {
        console.log(response);
        
        if (editionMode = 'add') {
         // this.products.unshift(product);
        } else {
          // const idx = this.products.findIndex(a => a.id === product.id);
          // this.products[idx] = product;
        }
      }
    );
  }

  alertInfo(){
    alert('welcome');
  }


}

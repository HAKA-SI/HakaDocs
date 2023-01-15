import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { SubCategoryModalComponent } from '../sub-category-modal/sub-category-modal.component';
import { User } from 'src/app/shared/models/user.model';
import { AuthService } from 'src/app/components/auth/auth.service';
import { take } from 'rxjs/operators';
import { ProductsService } from '../../products.service';
import { ConfirmService } from 'src/app/core/services/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';



@Component({
  selector: 'app-sub-category',
  templateUrl: './sub-category.component.html',
  styleUrls: ['./sub-category.component.scss'],
  animations: [SharedAnimations]
})
export class SubCategoryComponent implements OnInit {
  page: number = 1;
  searchText:string;
  public products = [];
  bsModalRef: BsModalRef;
  loggedUser: User;


  constructor(private productService: ProductsService, private modalService: BsModalService, private authService: AuthService
    , private confirmService: ConfirmService, private toastr: ToastrService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
  }

  ngOnInit() {
    this.getProducts();
  }

  showModal(editionMode: 'add' | 'edit', product: any | null) {
    console.log(editionMode);

    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        hakaDocClientId: this.loggedUser.haKaDocClientId,
        editionMode,
        product
      },
    };
    this.bsModalRef = this.modalService.show(SubCategoryModalComponent, config);
    this.bsModalRef.content.subCategoryModel.subscribe(
      (product: any) => {
        if (editionMode = 'add') {
          this.products.unshift(product);
        } else {
          const idx = this.products.findIndex(a => a.id === product.id);
          this.products[idx] = product;
        }
      }
    );
  }

  getProducts() {
    this.productService.getProductsWithDetailsList(this.loggedUser.haKaDocClientId).subscribe((response: any) => {
      this.products = response;
    });
  }

  deleteSubCategory(product) {
    this.confirmService.confirm('confirmation ','voulez vous vraiment supprimer cette sous categorie ?')
    .subscribe(result => {
      if(result) {
        this.productService.deleteProduct(this.loggedUser.haKaDocClientId,product.id).subscribe(() => {
          const idx = this.products.findIndex(a => a.id === product.id);
        this.products.splice(idx, 1);
        this.toastr.success("suppression éffectuée....");
        }, error => {
          this.toastr.error(error.message);
        })
      }
    })
  }

}

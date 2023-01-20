import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
import { AuthService } from 'src/app/components/auth/auth.service';
import { StoresService } from 'src/app/components/stores/stores.service';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
import { User } from 'src/app/shared/models/user.model';
import { Store } from 'src/app/_models/store.model';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { SubProductSN } from 'src/app/_models/subProductSN.model';
import { ProductsService } from 'src/app/components/products/products.service';



@Component({
  selector: 'app-stock-status',
  templateUrl: './stock-status.component.html',
  styleUrls: ['./stock-status.component.scss'],
  animations: [SharedAnimations]
})
export class StockStatusComponent implements OnInit {
  stores: Store[] = [];
  subProducts: SubProduct[] = [];
  subProductSNs: SubProductSN[] = [];
  storeId: number;
  storeSearchForm: FormGroup;
  noResult = '';
  searchText: string;
  page: number = 1;
  loggedUser: User;
  showSubProductSNsTable=false;





  constructor(private storeService: StoresService, private fb: FormBuilder, private authService: AuthService, private productService: ProductsService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.storeService.currentStore$.pipe(take(1)).subscribe((store) => {
      if (store) this.storeId = store.id;
    });
    this.createSearchForm();
  }

  ngOnInit(): void {
    if (!!this.storeId) {
      this.storeSearchForm.patchValue({ storeId: this.storeId });
      this.getSubProducts();
    }
    else {
      this.getStores();
    }

  }

  getStores() {
    this.storeService.storeList(this.loggedUser.haKaDocClientId).subscribe((response: Store[]) => {
      this.stores = response;
      if (response.length === 1) {
        this.storeSearchForm.patchValue({ storeId: this.storeId });
        this.getSubProducts();
      }

    });
  }

  createSearchForm() {
    this.storeSearchForm = this.fb.group({
      storeId: [null, Validators.required]
    });
  }

  getSubProducts() {
    this.noResult = '';
    const storeId = this.storeSearchForm.value.storeId;
    this.storeService.storeStock(storeId).subscribe((response: SubProduct[]) => {
      console.log('response', response);
      if (response.length > 0) this.subProducts = response;
      else this.noResult = 'aucun marariel trouvé dans ce magasin';
    })
  }

  getSubProductSNs(subProductId:number) {
    this.showSubProductSNsTable=true;
    this.subProductSNs=[];
    this.productService.subProductSNBySubProductId(this.loggedUser.haKaDocClientId,subProductId).subscribe((response:SubProductSN[]) => {
      this.subProductSNs = response;
    })
  }

  hideSubProductSnTable(){
    this.showSubProductSNsTable=false;
  }
}

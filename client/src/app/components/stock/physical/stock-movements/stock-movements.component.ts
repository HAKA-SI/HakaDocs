import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { AuthService } from 'src/app/_services/auth.service';
import { ProductsService } from 'src/app/components/products/products.service';
import { StoresService } from 'src/app/components/stores/stores.service';
import { ConfirmService } from 'src/app/core/services/confirm.service';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
import { User } from 'src/app/_models/user.model';
import { InvenOp } from 'src/app/_models/inventOp.model';
import { Store } from 'src/app/_models/store.model';
import { SubProductSN } from 'src/app/_models/subProductSN.model';

@Component({
  selector: 'app-stock-movements',
  templateUrl: './stock-movements.component.html',
  styleUrls: ['./stock-movements.component.scss'],
  animations: [SharedAnimations]
})
export class StockMovementsComponent implements OnInit {
  loggedUser: User;
  inventOps: InvenOp[] = [];
  stores: Store[] = [];
  showSubProductSNsTable = false;
  noResult = '';
  searchText: string;
  subProductSNs: SubProductSN[] = [];
  page: number = 1;
  inventOpId: number;
  subProductId: number;




  storeSearchForm: FormGroup;
  storeId: number;

  constructor(private authService: AuthService, private StoreService: StoresService, private fb: FormBuilder, private productService: ProductsService,
    private storeService: StoresService, private toastr: ToastrService, private confirmService: ConfirmService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.storeService.currentStore$.pipe(take(1)).subscribe((store) => {
      if (store) this.storeId = store.id;
    });
    this.createSearchForm();
  }

  ngOnInit(): void {
    if (!!this.storeId) {
      this.storeSearchForm.patchValue({ storeId: this.storeId });
      this.getInvenOps();
    }
    else {
      this.getStores();
    }

  }

  createSearchForm() {
    this.storeSearchForm = this.fb.group({
      storeId: [null, Validators.required]
    });
  }

  getStores() {
    this.storeService.storeList(this.loggedUser.haKaDocClientId).subscribe((response: Store[]) => {
      this.stores = response;
      if (response.length === 1) {
        this.storeSearchForm.patchValue({ storeId: this.storeId });
        this.getInvenOps();
      }

    });
  }

  getInvenOps() {
    this.noResult = '';
    const storeId = this.storeSearchForm.value.storeId;
    this.storeService.storeInventOps(this.loggedUser.haKaDocClientId, storeId).subscribe((invs) => {
      if (invs.length > 0)
        this.inventOps = invs;
      else
        this.noResult = 'aucun mouvemenent trouvé...';
    })
  }

  getSubProductSNs(inventOpId: number, subProductId) {
    this.inventOpId = inventOpId;
    this.subProductId = subProductId;
    this.showSubProductSNsTable = true;
    this.subProductSNs = [];
    this.productService.inventOpSubProductSNs(this.loggedUser.haKaDocClientId, inventOpId, subProductId).subscribe((response: SubProductSN[]) => {
      this.subProductSNs = response;
    })
  }

  hideSubProductSnTable() {
    this.showSubProductSNsTable = false;
  }

  deleteSubProductSN(subPorductSNId: number) {
    this.confirmService.confirm('confirmation ', 'voulez vous vraiment supprimer cette ce produit ?')
      .subscribe(result => {
        if (result) {
          this.productService.deleteInventOpSubProductSN(this.loggedUser.haKaDocClientId, this.inventOpId, subPorductSNId).subscribe(() => {
            const idx = this.subProductSNs.findIndex(a => a.id == subPorductSNId);
            this.subProductSNs.splice(idx, 1);
            this.toastr.success("suppression éffectuée...");
            this.getInvenOps();
          });
        }
      })
  }

  verifyDeletion(inventOp: InvenOp) {
    this.productService.canDeleteSubProductInventOp(this.loggedUser.haKaDocClientId, inventOp.toStoreId, inventOp.subProductId, inventOp.quantity).subscribe((response) => {
      
      if (response===false) {
        this.toastr.info("impossible d'effectuer cette suppression");
        return;
      }
      else {
        this.deleteInvenOp(inventOp.id, inventOp.subProductId);
      }
    })
  }

  deleteInvenOp(inventOpId: number, subProductId: number) {
    // throw new Error('Method not implemented.');
    this.confirmService.confirm('confirmation ', 'voulez vous vraiment ce transfert de stock ?')
      .subscribe(result => {
        if (result) {
          this.productService.deleteInventOpSubProduct(this.loggedUser.haKaDocClientId,inventOpId,subProductId).subscribe(() => {
            const idx = this.inventOps.findIndex(a => a.id == inventOpId);
            this.inventOps.splice(idx, 1);
            this.toastr.success("suppression éffectuée...");
         //   this.getInvenOps();
          })
        }
      })
  }

}

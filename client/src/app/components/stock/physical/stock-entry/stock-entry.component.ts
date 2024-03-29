import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { AuthService } from 'src/app/_services/auth.service';
import { ProductsService } from 'src/app/components/products/products.service';
import { StoresService } from 'src/app/components/stores/stores.service';
import { User } from 'src/app/_models/user.model';
import { Store } from 'src/app/_models/store.model';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-stock-entry',
  templateUrl: './stock-entry.component.html',
  styleUrls: ['./stock-entry.component.scss']
})
export class StockEntryComponent implements OnInit {

  subProductWithSn: SubProduct[] = [];
  subProductWithoutSns: SubProduct[] = [];
  physicalProductGroupId = environment.phisicalProductGroupId;
  loggedUser: User;
  stores: Store[] = [];
  showStoreSelection: boolean = true;



  constructor(private authService: AuthService, private router: Router, private productService: ProductsService, private storeService: StoresService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.router.routeReuseStrategy.shouldReuseRoute = () => false
  }

  ngOnInit(): void {
    this.getSubProducts();
    this.getStores();
  }

  getStores() {
    this.storeService.storeList(this.loggedUser.haKaDocClientId).subscribe((response: Store[]) => {
      this.stores = response;
    });
  }

  getSubProducts() {
    this.productService.getSubProducts(this.loggedUser.haKaDocClientId, this.physicalProductGroupId).subscribe((response: SubProduct[]) => {
      response.forEach(element => {
        if (element.withSerialNumber)
          this.subProductWithSn = [...this.subProductWithSn, element];
        else
          this.subProductWithoutSns = [...this.subProductWithoutSns, element];
      });
    });
  }

  reload(event) {
    this.router.navigateByUrl('/stock/physical/stock-entry', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/stock/physical/stock-entry']);
    });
  }

}

import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { AuthService } from 'src/app/components/auth/auth.service';
import { ConfirmService } from 'src/app/core/services/confirm.service';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
import { User } from 'src/app/shared/models/user.model';
import { productDB } from 'src/app/shared/tables/product-list';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { environment } from 'src/environments/environment';
import { ProductsService } from '../../products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  animations: [SharedAnimations]
})
export class ProductListComponent implements OnInit {

subProducts :SubProduct[]=[];
loggedUser:User;
page: number = 1;
searchText:string;
physicalProductGroupId = environment.phisicalProductGroupId;

  constructor(private productService: ProductsService, private toastr: ToastrService, private authService: AuthService,
    private confirmService: ConfirmService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));

  }

  ngOnInit() {
    this.getSubProducts();
  }
  getSubProducts() {
    this.productService.getSubProducts(this.loggedUser.haKaDocClientId,this.physicalProductGroupId).subscribe((response:SubProduct[]) => this.subProducts=response);
  }


}

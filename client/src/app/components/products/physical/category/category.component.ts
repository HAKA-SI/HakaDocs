import { Component, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/components/auth/auth.service';
import { User } from 'src/app/shared/models/user.model';
import { take } from 'rxjs/operators';
import { ProductsService } from '../../products.service';
import { Category } from 'src/app/_models/category.model';
import { TranslateService } from "@ngx-translate/core";
import { ConfirmService } from 'src/app/core/services/confirm.service';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
  animations: [SharedAnimations]
})
export class CategoryComponent implements OnInit {
  page: number = 1;
  public products = [];
  searchText: string;
  categories: Category[]=[];
  categoryName: string;
  loggedUser: User;
  closeResult = '';
  productCategoryId = environment.phisicalProductGroupId;

  constructor(private productService: ProductsService, private modalService: NgbModal, private toastr: ToastrService, private authService: AuthService,
    private translationService: TranslateService, private confirmService: ConfirmService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));

  }

  open(content,editionMode,categorie) {
    if(!!categorie)this.categoryName = categorie.name;

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      if (result === 'save') {
        if(editionMode==='add') this.createCategory();
        else this.editCategory(categorie);
      }
      else this.categoryName = '';

    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      this.categoryName = '';
    });
  }

  createCategory() {
    this.productService.createCategory(this.loggedUser.haKaDocClientId, this.categoryName).subscribe((response: Category) => {
      // this.typeEmps = [...this.typeEmps, element];
      this.categories = [response, ...this.categories];
      this.categoryName = '';

      this.toastr.success("enregistrement terminé...");
    }, (error) => this.toastr.error(error.message));
  }


  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.productService.categoryWithDetailsList(this.loggedUser.haKaDocClientId).subscribe((response: any) => {
      this.categories = response;

    })
  }

  // onEditConfirm

  deleteCategory(categorie: Category) {
    if (categorie.totalProducts > 0) {
      this.toastr.info("impossible de supprimer cette catégorie...");
      return;
    }

    this.confirmService.confirm('confirmation ', 'voulez vous vraiment supprimer cette categorie ?')
      .subscribe(result => {
        if (result) {
          this.productService.deleteCategory(this.loggedUser.haKaDocClientId, categorie.id).subscribe(() => {
            const idx = this.categories.findIndex(a => a.id === categorie.id);
            this.categories.splice(idx, 1);
            this.toastr.success("suppression éffectuée....");
          }, error => {
            this.toastr.error(error.message);
          })
        }
      })
  }

  editCategory(categorie) {
    this.productService.editCategory(this.loggedUser.haKaDocClientId, categorie.id, this.categoryName).subscribe(() => {
      const idx = this.categories.findIndex(a => a.id === categorie.id);
      this.productService[idx] = categorie;
      categorie.name=this.categoryName;
       this.categoryName='';
      this.toastr.success("modification terminée...");
    }, error => this.toastr.error(error.message));
  }

}

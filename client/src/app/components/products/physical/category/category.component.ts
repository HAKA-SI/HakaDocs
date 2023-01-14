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
  categories: Category[];
  categoryName: string;
  loggedUser: User;
  closeResult = '';

  constructor(private productService: ProductsService, private modalService: NgbModal, private toastr: ToastrService, private authService: AuthService,
    private translationService: TranslateService, private confirmService: ConfirmService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));

  }

  open(content) {
    // if(!!categorie) {
    //   this.selectedCategory = categorie;
    //   this.categoryName = categorie.name;
    // }
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      if (result === 'save') {
        this.save();
      }
      else this.categoryName = '';

    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      this.categoryName = '';
    });
  }

  save() {
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

  onEditConfirm($event: any) {

    const cat = $event.newData;
    this.editCategory(cat.id, cat.name, $event);
  }

  onDeleteConfirm($event: any) {
    if (+$event.data.totalProducts > 0) {
      this.toastr.info("impossible de supprimer cette catégorie...");
      return;
    }

    this.confirmService.confirm('confirmation ', 'voulez vous vraiment supprimer cette categorie ?')
      .subscribe(result => {
        if (result) {
          this.productService.deleteCategory(this.loggedUser.haKaDocClientId, $event.data.id).subscribe(() => {
            const all = $event.source.data;
            const newData = $event.data;
            const idx = all.findIndex(a => a.id === newData.id);
            all.splice(idx, 1);
            $event.source.load(all);
            this.toastr.success("suppression éffectuée....");
          }, error => {
            this.toastr.error(error.message);
          })
        }
      })
  }

  editCategory(categoryId, categoryName, event) {
    this.productService.editCategory(this.loggedUser.haKaDocClientId, categoryId, categoryName).subscribe(() => {
      const all = event.source.data;
      const newData = event.newData;
      const idx = all.findIndex(a => a.id === newData.id);
      all[idx] = newData;
      event.source.load(all);
      this.toastr.success("modification terminée...");
    }, error => this.toastr.error(error.message));
  }


}

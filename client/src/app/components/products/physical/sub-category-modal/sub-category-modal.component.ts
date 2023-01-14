import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ProductsService } from '../../products.service';

@Component({
  selector: 'app-sub-category-modal',
  templateUrl: './sub-category-modal.component.html',
  styleUrls: ['./sub-category-modal.component.scss']
})
export class SubCategoryModalComponent implements OnInit {
  @Output() subCategoryModel = new EventEmitter();
  subCategoryForm: FormGroup;
  hakaDocClientId: number;
  product: any;
  editionMode:'add'|'edit'='add';
  categories: any[] = [];

  constructor(private toastr:ToastrService,public bsModalRef: BsModalRef, private fb: FormBuilder, private productService: ProductsService) {
    this.createCategoryForm();
  }


  ngOnInit(): void {
    this.getCategories();
    if(this.editionMode==='edit') {
      this.subCategoryForm.patchValue(this.product);
    }
  }

  getCategories() {
    this.productService.categoryList(this.hakaDocClientId).subscribe((response: any[]) => {
      this.categories = response;
    })
  }

  createCategoryForm() {
    this.subCategoryForm = this.fb.group({
      categoryId: [null, Validators.required],
      name: ['', Validators.required]
    });
  }


  closeModal() {
    this.bsModalRef.hide();
  }

  save() {
    if(this.editionMode==='add') {
        this.createSubCategory();
        return;
    } else {
      this.editSubCategory();
    }
    
    // const storeData = this.storeForm.value;
    // const data={propValue : storeData.storeName};
    // this.storeService.createStore(+storeData.districtId,data).subscribe((product:any) => {
    //   this.createdStore.emit(product);
    //   this.bsModalRef.hide();
    // }, error => {
    //   console.log(error);
    // })
  }

  editSubCategory() {
    const formData = this.subCategoryForm.value;
   this.productService.editProduct(this.hakaDocClientId,this.product.id,formData.categoryId,formData.name).subscribe((product) => {
    this.subCategoryModel.emit(product);
          this.toastr.success("modification enregistrée...");
      this.bsModalRef.hide();
   })
  }

  createSubCategory() {
    const formData = this.subCategoryForm.value;
    this.productService.createProduct(this.hakaDocClientId,formData.categoryId,formData.name).subscribe((product) => {
          this.subCategoryModel.emit(product);
          this.toastr.success("enregistrment terminée...");
      this.bsModalRef.hide();

    })
  }



}

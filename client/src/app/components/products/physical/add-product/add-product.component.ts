import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { AuthService } from 'src/app/components/auth/auth.service';
import { User } from 'src/app/shared/models/user.model';
import { ProductsService } from '../../products.service';
@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent implements OnInit {
  public counter: number = 1;
  subCategories: any[] = [];
  subProductForm: FormGroup;
  // categories: Category[] = [];
  products: any[] = [];
  mainPhotoUrl = '';
  mainFile: File;
  otherFiles: File[] = [];
  otherFilesUrl = [];
  choices = [
    { value: false, label: 'NON' },
    { value: true, label: 'OUI' },
  ];
  loggedUser: User;
  public url = [{
    img: "assets/images/pro3/1.jpg",
  },
  {
    img: "assets/images/user.png",
  },
  {
    img: "assets/images/user.png",
  },
  {
    img: "assets/images/user.png",
  },
  {
    img: "assets/images/user.png",
  }
  ];
  subProduct: any;
  editionMode: 'add' | 'edit' = 'add';
  photoEdited:boolean=false;


  constructor(private router: Router,private route: ActivatedRoute, private toastr: ToastrService, private fb: FormBuilder, private authService: AuthService, private productService: ProductsService) {

    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.createProductForm();
  }

  ngOnInit() {
    const subProductId = this.route.snapshot.params['id'];
    if (!!subProductId) {

      this.editionMode = 'edit';
      this.productService.getSubProduct(subProductId, this.loggedUser.haKaDocClientId).subscribe((response) => {
        this.subProduct = response;
        this.subProductForm.patchValue(this.subProduct);
        if (!!this.subProduct.photoUrl) {
          this.url[0].img = this.subProduct.photoUrl;
        }
      })
    }
    this.getSubCategories();
    console.log(this.editionMode);
    
  }


  getSubCategories() {
    this.productService.productList(this.loggedUser.haKaDocClientId).subscribe((response: any[]) => this.subCategories = response);
  }

  createProductForm() {
    this.subProductForm = this.fb.group({
      productId: [null, Validators.required],
      name: ['', Validators.required],
      note: [''],
      unitPrice: [null, Validators.required],
      withSerialNumber: [null, Validators.required],
      quantityPerUnite: [null, Validators.required],
      reorderLevel: [1, Validators.required],
    })
  }

  //FileUpload
  readUrl(event: any, i) {
    if(this.editionMode==='edit') this.photoEdited=true;
    if (event.target.files.length === 0)
      return;
    this.mainFile = <File>event.target.files[0];
    //Image upload validation
    var mimeType = event.target.files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    // Image upload
    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (_event) => {
      this.url[i].img = reader.result.toString();
    }
  }




  save() {
    const formData = new FormData();
    const subproductData = this.subProductForm.value;
    // formData.append('categoryId', subproductData.categoryId);
    formData.append('productId', subproductData.productId);
    formData.append('name', subproductData.name);
    formData.append('unitPrice', subproductData.unitPrice);
    formData.append('withSerialNumber', subproductData.withSerialNumber);
    formData.append('quantityPerUnite', subproductData.quantityPerUnite);
    formData.append('reorderLevel', subproductData.reorderLevel);
    formData.append('note', subproductData.note);

    if (!!this.mainFile) {
      formData.append('mainPhotoFile', this.mainFile, this.mainFile.name);
    }
    for (let i = 0; i < this.otherFiles.length; i++) {
      const element = this.otherFiles[i];
      formData.append('otherPhotoFiles', element, element.name);
    }
    if (this.editionMode==='add') {
      this.saveSubProduct(formData);
    }
    else {
      this.editSubProduct(formData);
    }
  }

  saveSubProduct(formData: FormData) {
    this.productService.createSubProduct(this.loggedUser.haKaDocClientId, formData).subscribe(
      (product: any) => {
        this.mainFile = null;
        this.toastr.success("enregistrement terminée...");
        this.resetForm();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  editSubProduct(formData: FormData) {
   this.productService.editSubProduct(this.loggedUser.haKaDocClientId,this.subProduct.id,this.photoEdited,formData).subscribe(() => {
    this.toastr.success("modification enregistrée...");
    this.router.navigateByUrl('/products/physical/product-list');
   })
  }

  resetForm() {
    this.url[0] = { img: "assets/images/pro3/1.jpg" };
    this.subProductForm.reset();
  }


}

import { AfterContentInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/user.model';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { Store } from 'src/app/_models/store.model';
import { ProductsService } from '../../products/products.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/services/confirm.service';
import { StoresService } from '../../stores/stores.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-stock-entry-no-sn',
  templateUrl: './stock-entry-no-sn.component.html',
  styleUrls: ['./stock-entry-no-sn.component.scss']
})
export class StockEntryNoSnComponent implements OnInit {

  mainPhotoUrl = '';
  mainFile: File;
  otherFiles: File[] = [];
  otherFilesUrl = [];
  @Output() done = new EventEmitter();
  @Input() stockEntryForm: FormGroup;
  @Input() loggedUser: User;
  @Input() stores: Store[] = [];
  @Input() subproducts: SubProduct[] = [];
  storeId: number;

  constructor(private productService: ProductsService, private storeService: StoresService, private fb: FormBuilder, private confirmService: ConfirmService, private toastr: ToastrService) {
    this.storeService.currentStore$.pipe(take(1)).subscribe((store) => {
      if (store) this.storeId = store.id;
    });
    this.createStockEntryForm();
  }

  ngOnInit(): void {
    //si un seul store, on affiche cache la selection du store et et affecte le storeid
    if (!!this.storeId) {
      this.stockEntryForm.patchValue({ storeId: this.storeId });
    }
  }

  createStockEntryForm() {
    this.stockEntryForm = this.fb.group({
      storeId: [null, Validators.required],
      mvtDate: [null, Validators.required],
      subProductId: [null, Validators.required],
      quantity: [null, Validators.required],
      refNum: [''],
      note: [''],
    });
  }

  mainImgResult(event: any) {
    this.mainFile = <File>event.target.files[0];
    // recuperation de l'url de la photo
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.mainPhotoUrl = e.target.result;
    };
    reader.readAsDataURL(event.target.files[0]);
  }

  getOtherFilesResult(event) {
    this.otherFilesUrl = [];
    let files = event.target.files;
    this.otherFiles = files;
    if (files) {
      for (let file of files) {
        let reader = new FileReader();
        reader.onload = (e: any) => {
          this.otherFilesUrl.push(e.target.result);
        }
        reader.readAsDataURL(file);
      }
    }
  }

  save() {

    // console.log(JSON.stringify(this.stockEntryForm.value));

    const msg = "confirmez-vous l'entrée en stock dans le magasin ?";
    this.confirmService.confirm("confirmation", msg).subscribe((result) => {
      if (result) {

        const formValues = this.stockEntryForm.value;
        const formData = new FormData();
        if (!!this.mainFile) {
          formData.append('mainPhotoFile', this.mainFile, this.mainFile.name);
        }
        for (let i = 0; i < this.otherFiles.length; i++) {
          const element = this.otherFiles[i];
          formData.append('otherPhotoFiles', element, element.name);
        }

        formData.append('storeId', formValues.storeId);
        formData.append('subProductId', formValues.subProductId);
        formData.append('sn', formValues.sn);
        formData.append('quantity', formValues.quantity);
        formData.append('refNum', formValues.refNum);
        formData.append('note', formValues.note);
        var datestr = (new Date(formValues.mvtDate)).toUTCString();
        formData.append('mvtDate', datestr);
        console.log(formValues);


        this.productService.createSubProductsWithoutSN(this.loggedUser.haKaDocClientId, formData).subscribe(() => {
          
          //this.resetForm();
          this.toastr.success("enregistrement terminé...");
          this.done.emit(true);

        }, error => this.toastr.error(error))
      }
    })
  }

  resetForm() {
    this.stockEntryForm.reset();
    this.mainFile = null;
    this.otherFiles = [];
    this.mainPhotoUrl = '';
    this.otherFilesUrl = [];
  }


}

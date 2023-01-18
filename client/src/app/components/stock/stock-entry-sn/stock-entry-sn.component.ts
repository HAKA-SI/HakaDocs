import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { ConfirmService } from 'src/app/core/services/confirm.service';
import { User } from 'src/app/shared/models/user.model';
import { Store } from 'src/app/_models/store.model';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { ProductsService } from '../../products/products.service';
import { StoresService } from '../../stores/stores.service';

@Component({
  selector: 'app-stock-entry-sn',
  templateUrl: './stock-entry-sn.component.html',
  styleUrls: ['./stock-entry-sn.component.scss']
})
export class StockEntrySnComponent implements OnInit {

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
    this.addSn();
  }


  ngOnInit(): void {
    if (!!this.storeId) {
      this.stockEntryForm.patchValue({ storeId: this.storeId });
    }
  }

  createStockEntryForm() {
    this.stockEntryForm = this.fb.group({
      storeId: [null, Validators.required],
      mvtDate: [null, Validators.required],
      subProductId: [null, Validators.required],
      refNum: [''],
      note: [''],
      snList: this.fb.array([]),
    });
  }

  get sns() {
    return this.stockEntryForm.controls['snList'] as FormArray;
  }

  addSn() {
    const snForm = this.fb.group({
      sn: ['', Validators.required],
    });
    this.sns.push(snForm);
  }

  deleteSn(lessonIndex: number) {
    this.sns.removeAt(lessonIndex);
  }

  save() {

    const msg = "confirmez-vous l'entrée en stock dans le magasin";
    this.confirmService.confirm('confirmation', msg).subscribe((result) => {
      if (result) {
        const formValues = this.stockEntryForm.value;
        // const formData = new FormData();
        // if(!!this.mainFile) {
        //   formData.append('mainPhotoFile', this.mainFile, this.mainFile.name);
        // }
        // for (let i = 0; i < this.otherFiles.length; i++) {
        //   const element = this.otherFiles[i];
        //   formData.append('otherPhotoFiles', element, element.name);
        // }

        const snList = [];
        for (let index = 0; index < formValues.snList.length; index++) {
          const element = formValues.snList[index];
          // formData.append('snList', element.sn);
          snList.push(element.sn);
        }
        formValues.sns = snList;

        // formData.append('storeId', formValues.storeId);
        // formData.append('subProductId', formValues.subProductId);
        // formData.append('sns', formValues.sns);
        // formData.append('refNum', formValues.refNum);
        // formData.append('note', formValues.note);
        // var datestr = new Date(formValues.mvtDate).toUTCString();
        // formData.append('mvtDate', datestr);
        // formValues.mvtDate = datestr;

        this.productService.createSubProductsWithSN(this.loggedUser.haKaDocClientId,formValues).subscribe(() => {
          this.resetForm();
          this.toastr.success("enregistrement terminé...");
          // this.mainFile = null;
          // this.otherFiles= [];
          // this.mainPhotoUrl='';
          // this.otherFilesUrl=[];
        }, error => this.toastr.error(error))
      }
    });
  }
  resetForm() {
    this.stockEntryForm.reset();
  }


}

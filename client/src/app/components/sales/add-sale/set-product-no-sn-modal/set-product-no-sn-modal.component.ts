import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { SubProduct } from 'src/app/_models/subProduct.model';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';

@Component({
  selector: 'app-set-product-no-sn-modal',
  templateUrl: './set-product-no-sn-modal.component.html',
  styleUrls: ['./set-product-no-sn-modal.component.scss'],
  animations: [SharedAnimations]
})
export class SetProductNoSnModalComponent implements OnInit {
  @Output() subProductData: any = new EventEmitter();
  // editionMode: 'add' | 'edit' = 'add';
  subProduct: any;
  subProductForm: FormGroup;
  quantityExceed: boolean;

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) {
    
  }


  ngOnInit(): void {
    this.createSubProductForm();
    this.subProductForm.controls['newqty'].valueChanges.subscribe(value => {
      if (!!value && +this.subProduct.quantity < value)
        this.quantityExceed = true;
      else
        this.quantityExceed = false;
    });
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  createSubProductForm() {
    this.subProductForm = this.fb.group({
      discount: [this.subProduct.discount],
      newqty: [this.subProduct.newqty, [Validators.required,Validators.min(1)]]
    });
  }

  save() {
    const formdata = this.subProductForm.value;
    formdata.id = this.subProduct.id;
    // formdata.quantity = formdata.newqty;
    formdata.unitPrice=this.subProduct.unitPrice;
    formdata.name=this.subProduct.name;
    formdata.withSerialNumber=this.subProduct.withSerialNumber;
    formdata.category=this.subProduct.category;
    this.subProductData.emit(formdata);
    this.bsModalRef.hide();
  }


}

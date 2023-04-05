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
  @Output() subProductData: any = new EventEmitter();;
  editionMode: 'add' | 'edit' = 'add';
  subProduct: SubProduct;
  subProductForm: FormGroup;
  quantityExceed: boolean;

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) {
    this.createSuvProductForm();
  }


  ngOnInit(): void {
    this.subProductForm.controls['quantity'].valueChanges.subscribe(value => {
      if (!!value && +this.subProduct.quantity < value)
        this.quantityExceed = true;
      else
        this.quantityExceed = false;
    });
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  createSuvProductForm() {
    this.subProductForm = this.fb.group({
      discount: [0],
      quantity: [null, Validators.required]
    });
  }

  save() {
    const formdata = this.subProductForm.value;
    formdata.id = this.subProduct.id;
    this.subProductData.emit(formdata);
    this.bsModalRef.hide();
  }


}

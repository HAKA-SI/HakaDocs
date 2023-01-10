import { Component, OnInit } from '@angular/core';
import { categoryDB } from '../../../../shared/tables/category';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  public closeResult: string;
  public categories = []
  categoryName:string;

  constructor(private modalService: NgbModal,private toastr: ToastrService) {
    this.categories = categoryDB.category;
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      if(result==='save'){
        this.save();
      } 
      else  this.categoryName='';
      
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      this.categoryName='';
    });
  }

  save() {
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


  public settings = {
    actions: {
      position: 'right'
    },
    columns: {
      img: {
        title: 'Image',
        type: 'html',
      },
      product_name: {
        title: 'Name'
      },
      price: {
        title: 'Price'
      },
      status: {
        title: 'Status',
        type: 'html',
      },
      category: {
        title: 'Category',
      }
    },
  };

  ngOnInit() {
  }

}

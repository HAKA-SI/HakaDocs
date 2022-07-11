import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
busyRequestCount=0;
  constructor(private spinnerService: NgxSpinnerService) { }

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'timer',
      bdColor:'rgba(0,0,0,0.5)',
      color:'#f64c67',
      fullScreen:true
    })
  }

  idle() {
    this.busyRequestCount--;
    if(this.busyRequestCount<=0) {
      this.busyRequestCount=0;
      this.spinnerService.hide();
    }
  }
}

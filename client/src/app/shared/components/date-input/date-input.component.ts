import { ChangeDetectionStrategy, Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DateInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('f', { static: true }) input: ElementRef;
  @Input() label: string;


  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit() {
    // const control = this.controlDir.control;
    // const validators = control.validator ? [control.validator] : [];
    // const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

    // control.setValidators(validators);
    // control.setAsyncValidators(asyncValidators);
    // control.updateValueAndValidity();
  }

  onChange(event) { }

  onTouched() { }

  writeValue(value: any): void {
    // this.input.nativeElement.value = value || '';
    //this.input._elRef.nativeElement.value = value || '';
    console.log(this.input);
    
    // this.renderer.setProperty(this.input.nativeElement, 'value', value);
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }


}

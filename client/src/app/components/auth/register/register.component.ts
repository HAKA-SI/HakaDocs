import { ToastrService } from 'ngx-toastr';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
registerForm : FormGroup;
errors : string[];
showConfirmation=false;
// @ViewChild('card', { static: false }) card: ElementRef<HTMLDivElement>;
// @ViewChild('confirm', { static: true }) confirmDiv: ElementRef<HTMLDivElement>;
owlcarousel = [
  {
    title: "Welcome to Multikart",
    desc: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy.",
  },
  {
    title: "Welcome to Multikart",
    desc: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy.",
  },
  {
    title: "Welcome to Multikart",
    desc: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy.",
  }
]
owlcarouselOptions = {
  loop: true,
  items: 1,
  dots: true
};
  constructor(private fb: FormBuilder, private authService: AuthService, private router :  Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      lastName : [null,[ Validators.required]],
      firstName : [null,[ Validators.required]],
      phoneNumber : [null,[ Validators.required,Validators.minLength(10),Validators.maxLength(10)]],
      email : [null, [Validators.required, Validators.required,Validators
        .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],[this.validateEmailNotTaken()]],
      // password : [null,[ Validators.required, Validators.minLength(8)]],
    })
  }


  onSubmit() {
    this.showConfirmation=true;

    this.authService.register(this.registerForm.value).subscribe(() => {
      this.toastr.success("enregistrement terminée..");
      this.showConfirmation=true;
    },error => {
     this.errors = error.error;
    })

  }


  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.authService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? { emailExists: true } : null;
            })
          );
        })
      );
    };
  }

}

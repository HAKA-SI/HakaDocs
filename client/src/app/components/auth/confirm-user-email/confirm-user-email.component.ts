import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user.model';
import { AccountsService } from 'src/app/_services/accounts.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-confirm-user-email',
  templateUrl: './confirm-user-email.component.html',
  styleUrls: ['./confirm-user-email.component.scss']
})
export class ConfirmUserEmailComponent implements OnInit {
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
  loginForm: FormGroup;
  userNameExist = false;
  user: User;





  constructor(private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private authService: AuthService,
    private toastr: ToastrService, private accountService: AccountsService) { }

  ngOnInit(): void {
    const validationCode = this.route.snapshot.params['code'];

    if (!!validationCode) {
      this.createUserForm();
      this.accountService.confirmEmail(validationCode).subscribe((user: User) => {
        this.user = user;
       
      }, error => {
        this.toastr.error(error.rror);
        this.router.navigate(['/']);
      })

      this.authService.logout(false);

    }
    else this.router.navigateByUrl('/');
  }

  createUserForm() {
    this.loginForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      checkPassword: [null, [Validators.required, this.confirmationValidator]],
    });
  }


  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.loginForm.controls.password.value) {
      return { confirm: true, error: true };
    }
  }


  userNameVerification() {
    const userName = this.loginForm.value.userName;
    if (!!userName) {
      this.userNameExist = false;

      this.accountService.userNameExist(userName).subscribe((res: boolean) => {

        if (res) this.userNameExist = true;
        else this.userNameExist = false;
      });
    }
  }

  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.loginForm.controls.checkPassword.updateValueAndValidity());
  }



  save() {
    const formData = this.loginForm.value;
    this.authService.setUserLoginPassword(this.user.id, formData).subscribe(() => {
      // if (this.userPhotoUrl) {
      //   this.addPhoto(this.user.id);
      // }
      // this.passwordSetingResult.emit(true);
      this.toastr.success('enregistrement terminé...');
      this.router.navigate(['/dashboard']);

    }, error => {
      this.toastr.error(error);
      this.router.navigate(['error']);
    });
  };

}

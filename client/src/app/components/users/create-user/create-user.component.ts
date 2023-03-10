import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { timer, of } from 'rxjs';
import { map, switchMap, take } from 'rxjs/operators';
import { User } from 'src/app/_models/user.model';
import { AccountsService } from 'src/app/_services/accounts.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {
  public accountForm: FormGroup;
  public permissionForm: FormGroup;
  loggedUser: User;

  constructor(private formBuilder: FormBuilder, private acccountService: AccountsService, private toastr: ToastrService,
    private authService: AuthService, private accountService: AccountsService) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => (this.loggedUser = user));
    this.createAccountForm();
    this.createPermissionForm();
  }

  createAccountForm() {
    this.accountForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: [null, 
        [Validators.required, Validators
        .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]
      ],
    })
  }
  createPermissionForm() {
    this.permissionForm = this.formBuilder.group({
    })
  }

  createAccount() {
    const accountToCreate = this.accountForm.value;
    this.acccountService.createAccount(this.loggedUser.haKaDocClientId, accountToCreate).subscribe(() => {
      this.toastr.success('enregistrement terminé...');
      this.accountForm.reset();
    })

  }

  ngOnInit() {
  }


  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.acccountService.checkEmailExists(this.loggedUser.haKaDocClientId,control.value).pipe(
            map(res => {
              if(res===true) this.toastr.info('ce email est deja utilisé...');
               return res ? {emailExists: true} : null;
            })
          );
        })
      )
    }
  }

}

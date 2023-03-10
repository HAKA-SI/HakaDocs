import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user.model';
import { AuthService } from '../../../_services/auth.service';
import { AccountsService } from '../../../_services/accounts.service';

@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styleUrls: ['./create-role.component.scss']
})
export class CreateRoleComponent implements OnInit {
  loggedUser: User;
  public roleForm: FormGroup;

  constructor(private formBuilder: FormBuilder,private accountService: AccountsService,private authService: AuthService, private toastr: ToastrService) {
    this.authService.currentUser$
    .pipe(take(1))
    .subscribe((user) => (this.loggedUser = user));
    this.createRoleForm();

  }

  ngOnInit(): void {
  }

  createRoleForm() {
    this.roleForm = this.formBuilder.group({
      roleName: ['',Validators.required]
    });
  }

  save(){
    const formdata = this.roleForm.value
    this.accountService.createRole(this.loggedUser.haKaDocClientId,formdata.roleName).subscribe(() => {
      this.toastr.success("enregistrement terminé...");
      this.roleForm.reset();
    });
  }



}

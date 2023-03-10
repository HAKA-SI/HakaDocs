import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user.model';
import { AuthService } from '../../../_services/auth.service';
import { AccountsService } from '../../../_services/accounts.service';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.scss']
})
export class RolesListComponent implements OnInit {
  public role_list = [];
  public selected = [];
  loggedUser: User;
  constructor(private accountService:AccountsService,private authService: AuthService) {
    this.authService.currentUser$
    .pipe(take(1))
    .subscribe((user) => (this.loggedUser = user));
   }

  ngOnInit(): void {
    this.getRoles();
  }
  getRoles() {
  this.accountService.getRoleList(this.loggedUser.haKaDocClientId).subscribe((response:any[]) =>{
    this.role_list = response;
  })
  }

  onSelect({ selected }) {
    this.selected.splice(0, this.selected.length);
    this.selected.push(...selected);
  }

}

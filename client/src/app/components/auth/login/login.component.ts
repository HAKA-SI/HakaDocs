import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiNotificationService } from 'src/app/_services/api-notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  public registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private authService: AuthService,
    private toastr: ToastrService, private router: Router, private apiNotificationService : ApiNotificationService) {
    this.createLoginForm();
  }

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

  createLoginForm() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    })
  }



  ngOnInit() {
  }

  login() {
    this.authService.login(this.loginForm.value).subscribe(
      (response) => {
        this.apiNotificationService.startPolling();
        this.router.navigateByUrl('/dashboard');
        
      },
      (error) => {
        this.toastr.error('login ou mot de passe incorrect');
      }
    );
  }

}

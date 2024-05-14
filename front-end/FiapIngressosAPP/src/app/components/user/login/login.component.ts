import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginUser } from '../../../models/user/loginUser';
import { AccountService } from '../../../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginUser = {} as LoginUser;

  constructor(private accountService:AccountService, private router:Router, private toaster:ToastrService) { }

  ngOnInit(): void {}

  public login():void {
    this.accountService.login(this.loginUser as LoginUser).subscribe(
      () => {this.router.navigateByUrl('/home')},
      (error:any) => {
        if(error.status == 401)
        {
          this.toaster.error('Usuário ou senha inválidos', 'Erro');
        }
        else
        {
          this.toaster.error('Usuário ou senha inválidos', 'Erro');
          console.error(error)
        }
      }
    );
  }
}

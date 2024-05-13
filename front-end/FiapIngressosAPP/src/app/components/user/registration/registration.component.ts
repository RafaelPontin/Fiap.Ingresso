import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "../../../services/account.service";
import { Router } from "@angular/router";
import { CadastroUser } from "../../../models/user/cadastroUser";
import { Component, OnInit } from "@angular/core";
import { ValidatorField } from "../../../helpers/ValidatorField";
import { ToastrService } from "ngx-toastr";


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  user = {} as CadastroUser;

  form!: FormGroup;

  constructor(private fb: FormBuilder, private accountService:AccountService, private router : Router,  private toaster: ToastrService) { }

  get f(): any { return this.form.controls; }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmePassword')
    };

    this.form = this.fb.group({
      nome: ['', Validators.required],
      cpf: ['', Validators.required],
      email: ['',
        [Validators.required, Validators.email]
      ],
      password: ['',
        [Validators.required, Validators.minLength(6)]
      ],
      confirmePassword: ['', Validators.required],
    }, formOptions);
  }

  register():void {
    this.user ={...this.form.value};
    this.accountService.post(this.user).subscribe(
      ()=> this.router.navigateByUrl('/'),
      (error: any)=> this.toaster.error(error.error)
    )
  }

}

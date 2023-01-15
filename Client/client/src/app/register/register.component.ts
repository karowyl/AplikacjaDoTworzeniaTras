import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.model.country = "Polska";
  }

  register(){
    if(!this.model.password || this.model.password.length < 4 || this.model.password.length > 8)
    {
      this.toastr.error("Hasło jest polem wymaganym, musi posiadać od 4 do 8 znaków");
    }
    else if(!this.model.username)
    {
      this.toastr.error("Nazwa użytkownika jest polem wymaganym");
    }
    else
    {
      this.accountService.register(this.model).subscribe(response => {
        this.cancel();
      }, error => {
        var errors : string = "";
        if(Array.isArray(error))
        {
          error.forEach(x => {
            errors = errors + "\n" + x
          })
          this.toastr.error(errors);
        }
        else
        {
          this.toastr.error(error.error);
        }
        
      })
    }
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}

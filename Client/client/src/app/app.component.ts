import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TimeagoIntl } from 'ngx-timeago';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { strings as stringsPl } from 'ngx-timeago/language-strings/pl';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Runner app';
  users: any;

  constructor( private accountService: AccountService, intl: TimeagoIntl) {
    intl.strings = stringsPl;
    intl.changes.next();
  }
  name = 'Angular';
  date = new Date();
  
  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

}

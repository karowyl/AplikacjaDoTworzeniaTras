import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users', httpOptions);
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username, httpOptions);
  }

  findMembers(text: string) {
    return this.http.get<Member[]>(this.baseUrl + 'users/find/' + text, httpOptions);
  }

  updatemember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member, httpOptions)
  }

  getFriends() {
    return this.http.get<Member[]>(this.baseUrl + 'friends', httpOptions);
  }

  addFriend(id: number) {
    return this.http.post(this.baseUrl + 'friends/addFriend/' + id, httpOptions);
  }

  removeFriend(id: number) {
    return this.http.post(this.baseUrl + 'friends/removeFriend/' + id, httpOptions);
  }

  getHttpOptions() {
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user = JSON.parse(userString);
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token
      })
    }
  }
}

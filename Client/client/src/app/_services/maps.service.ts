import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Map } from '../_models/map';
import { Comment } from '../_models/comment';

const httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
    })
  }
  
@Injectable({
  providedIn: 'root'
})
export class MapsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addRoute(map: Map){
    return this.http.post(this.baseUrl + 'map/createMap', map, httpOptions);
  }

  getAllRoutes() {
    return this.http.get<Map[]>(this.baseUrl + 'map/getAllMaps', httpOptions);
  }

  getMyRoutes() {
    return this.http.get<Map[]>(this.baseUrl + 'map/getMyMaps', httpOptions);
  }
  getRoutesInTheCity(city: string){
    return this.http.get<Map[]>(this.baseUrl + 'map/getMapsInCity/' + city, httpOptions);
  }

  addComment(comment: Comment){
    return this.http.post(this.baseUrl + 'map/addComment', comment, httpOptions);
  }

  getComments(mapId: number){
    return this.http.get<Comment[]>(this.baseUrl + 'map/getComments/' + mapId, httpOptions);
  }

  getRoutesCreatedByUser(username: string){
    return this.http.get<Map[]>(this.baseUrl + 'map/getMapsCreatedBy/' + username, httpOptions);
  }
}
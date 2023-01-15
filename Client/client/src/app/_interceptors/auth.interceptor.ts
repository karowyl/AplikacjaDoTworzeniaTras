import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../_models/user";

@Injectable({
    providedIn: 'root'
  })
  export class AuthInterceptor implements HttpInterceptor {
  
    constructor() {}
  
    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
  
      const user = JSON.parse(localStorage.getItem("user")) as User;

      if (user) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${user.token}`
          }
        })
      }
      return next.handle(request);
    }
  }
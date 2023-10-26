import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {

  constructor(private http: HttpClient) {}

  logIn(email: string, password: string): Observable<any> {
    return this.http.post('https://localhost:7207/login',
      {
        "email": email,
        "password": password
      }
    )
  }

  logOut(){
    localStorage.setItem("activeToken", "");
  }

  storeToken(newToken: string){
    localStorage.setItem("activeToken", newToken);
  }

  getToken(){
    return localStorage.getItem("activeToken");
  }

  signUp(email: string, password: string, address: string): Observable<any> {
    return this.http.post('https://localhost:7207/user',
      {
        "email": email,
        "password": password,
        "address": address
      }
    )
  }
}

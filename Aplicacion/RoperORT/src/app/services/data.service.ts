import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  activeToken: string = "";

  constructor(private http: HttpClient) {}

  logIn(email: string, password: string): Observable<any> {
    return this.http.post('https://localhost:7207/login',
      {
        "email": email,
        "password": password
      }
    )
  }

  storeToken(newToken: string){
    this.activeToken = newToken;
  }
}

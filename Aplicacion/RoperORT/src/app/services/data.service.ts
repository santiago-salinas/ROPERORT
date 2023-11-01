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

  getUser(): Observable<any>{
    return this.http.get('https://localhost:7207/user',
      {
        headers: { "Auth": this.getToken() || "" }
      }
    )
  }

  updateUser(email: string, password: string, address: string): Observable<any>{
    return this.http.put('https://localhost:7207/user',
    {
      "email": email,
      "password": password,
      "address": address
    },
    {
      headers: { "Auth": this.getToken() || "" }
    }
    )
  }

  getAllUsers(): Observable<any>{
    return this.http.get('https://localhost:7207/admin/users',
    { headers: { "Auth": this.getToken() || "" } }
    )
  }

  deleteUserById(id: number): Observable<any>{
    return this.http.delete('https://localhost:7207/admin/users/' + id,
    { headers: { "Auth": this.getToken() || "" } }
    )
  }

  createUser(email: string, password: string, address: string, roles: string[]): Observable<any>{
    return this.http.post('https://localhost:7207/admin/users',
    {
      "email": email,
      "password": password,
      "address": address,
      "roles": roles.map(name => ({ name: name }))
    },
    {
      headers: { "Auth": this.getToken() || "" }
    }
    )
  }

  getUserById(id: string): Observable<any>{
    return this.http.get('https://localhost:7207/admin/users/' + id,
    {
      headers: { "Auth": this.getToken() || "" }
    })
  }

  updateUserById(id: string, email: string, password: string, token: string, 
      address: string, roles: string[]): Observable<any>{
    return this.http.put('https://localhost:7207/admin/users/' + id,
    {
      "id": id,
      "email": email,
      "password": password,
      "token": token,
      "address": address,
      "roles": roles.map(name => ({ name: name }))
    },
    {
      headers: { "Auth": this.getToken() || "" }
    })
  }

  deleteUser(): Observable<any>{
    return this.http.delete('https://localhost:7207/user',
    {
      headers: { "Auth": this.getToken() || "" }
    }
    )
  }
}

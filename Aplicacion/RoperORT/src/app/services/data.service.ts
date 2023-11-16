import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environments.prod';

@Injectable({
  providedIn: 'root',
})
export class LoginService {

  constructor(private http: HttpClient) {}

  logIn(email: string, password: string): Observable<any> {
    return this.http.post(environment.baseUrl+'login',
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
    return this.http.post(environment.baseUrl+'user',
      {
        "email": email,
        "password": password,
        "address": address
      }
    )
  }

  getUser(): Observable<any>{
    return this.http.get(environment.baseUrl+'user',
      {
        headers: { "Auth": this.getToken() || "" }
      }
    )
  }

  updateUser(email: string, password: string, address: string): Observable<any>{
    return this.http.put(environment.baseUrl+'user',
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
    return this.http.get(environment.baseUrl+'admin/users',
    { headers: { "Auth": this.getToken() || "" } }
    )
  }

  deleteUserById(id: number): Observable<any>{
    return this.http.delete(environment.baseUrl+'admin/users/' + id,
    { headers: { "Auth": this.getToken() || "" } }
    )
  }

  createUser(email: string, password: string, address: string, roles: string[]): Observable<any>{
    return this.http.post(environment.baseUrl+'admin/users',
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
    return this.http.get(environment.baseUrl+'admin/users/' + id,
    {
      headers: { "Auth": this.getToken() || "" }
    })
  }

  updateUserById(id: string, email: string, password: string, token: string, 
      address: string, roles: string[]): Observable<any>{
    return this.http.put(environment.baseUrl+'admin/users/' + id,
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
    return this.http.delete(environment.baseUrl+'user',
    {
      headers: { "Auth": this.getToken() || "" }
    }
    )
  }

  isAdmin(): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      this.getUser().subscribe(
        (data) => {
          for (let i = 0; i < data.roles.length; i++) {
            const role = data.roles[i].name;
            if (role === "Admin") {
              resolve(true);
              return;
            }
          }
          resolve(false);
        },
        (error) => {
          reject(error);
        }
      );
    });
  }
  
}

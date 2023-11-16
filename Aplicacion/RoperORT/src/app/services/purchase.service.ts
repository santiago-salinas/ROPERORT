import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from './data.service';
import { environment } from '../../environments/environments.prod';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  constructor(private http: HttpClient, private service: LoginService) { }

  getThisUsersPurchases(): Observable<any>{
    return this.http.get(environment.baseUrl+'purchase/history',
    {
      headers: { "Auth": this.service.getToken() || "" }
    }
  )
  }

  getAllPurchases(): Observable<any>{
    return this.http.get(environment.baseUrl+'admin/purchases',
    {
      headers: { "Auth": this.service.getToken() || "" }
    }
  )
  }
}

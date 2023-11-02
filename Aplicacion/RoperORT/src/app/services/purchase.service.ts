import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  constructor(private http: HttpClient, private service: LoginService) { }

  getThisUsersPurchases(): Observable<any>{
    return this.http.get('https://localhost:7207/purchase/history',
    {
      headers: { "Auth": this.service.getToken() || "" }
    }
  )
  }

  getAllPurchases(): Observable<any>{
    return this.http.get('https://localhost:7207/admin/purchases',
    {
      headers: { "Auth": this.service.getToken() || "" }
    }
  )
  }
}

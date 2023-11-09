import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Purchase } from 'src/app/models/purchase.model';
import { PurchaseService } from 'src/app/services/purchase.service';
import { PurchaseCardComponent } from 'src/app/reusable/purchase-card/purchase-card.component';

@Component({
  selector: 'app-admin-purchases',
  templateUrl: './admin-purchases.component.html',
  styleUrls: ['./admin-purchases.component.scss'],
  standalone: true,
  imports: [ CommonModule,PurchaseCardComponent ]
})
export class AdminPurchasesComponent {
  purchaseList: Purchase[] = [];
  service: PurchaseService;

  constructor(private dataService: PurchaseService, private _snackBar: MatSnackBar){
    this.service = dataService;
  }

  ngOnInit(): void {
    this.dataService.getAllPurchases().subscribe(
      (data: Purchase[]) => {
        this.purchaseList = data;
      },
      (error:any) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );
  }

  showSnackbar(message: string, action: string, duration: number){
    this._snackBar.open(message, action, {
      duration: duration,
    });
  }
}

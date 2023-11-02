import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatCardModule} from '@angular/material/card';
import { Router } from '@angular/router';
import { PurchaseService } from 'src/app/services/purchase.service';
import { Purchase } from 'src/app/models/purchase.model';

@Component({
  selector: 'app-purchase-card',
  templateUrl: './purchase-card.component.html',
  styleUrls: ['./purchase-card.component.scss'],
  standalone: true,
  imports: [ CommonModule, MatCardModule ]
})
export class PurchaseCardComponent {
  service: PurchaseService;

  constructor(private router: Router, private dataService: PurchaseService) {
    this.service = dataService;
  }

  @Input() purchase: Purchase = new Purchase();

  formatDate(dateTime: string): string{
    let date = dateTime.split("T")[0];
    let specificDateInfo = date.split("-");
    let year = specificDateInfo[0];
    let month = specificDateInfo[1];
    let day = specificDateInfo[2];
    let finalDate = day + "/" + month + "/" + year;
    return finalDate;
  }
}

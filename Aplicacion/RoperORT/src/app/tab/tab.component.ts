import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { ProductService } from '../product.service';
import { ProductCardComponent } from '../product-card/product-card.component';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-tab',
  templateUrl: './tab.component.html',
  styleUrls: ['./tab.component.scss'],
  standalone: true,
  imports: [CommonModule,MatTabsModule,ProductCardComponent,LoginComponent],
})

export class TabComponent {
  data: any;
  text="";

  constructor(private dataService: ProductService) {
  }

  getUsers(){
    return "Usuarios";
  }

  ngOnInit(): void {
    this.dataService.getProducts().subscribe(
      (data) => {
        console.log(data);
        this.data = data;
      },
      (error) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );
  }

  getNameList(colors: any[]): string {
    return colors.map((color) => color.name).join(', ');
  }
}

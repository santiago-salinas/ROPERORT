import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef} from '@angular/core';
import { LoginService } from '../data.service';
import { ProductCardComponent } from '../product-card/product-card.component';
import { TabComponent } from '../tab/tab.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,ProductCardComponent,TabComponent],
  template: `
    <app-tab></app-tab>
  `,
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  data: any;

  constructor(private dataService: LoginService) {
  }

  ngOnInit(): void { }

  getNameList(colors: any[]): string {
    return colors.map((color) => color.name).join(', ');
  }
}

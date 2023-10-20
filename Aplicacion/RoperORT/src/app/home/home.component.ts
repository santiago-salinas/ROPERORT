import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  template: `
    <p>
      home works!
    </p>
  `,
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  data: any;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.dataService.getData().subscribe((data) => {
      console.log(data);
    });
  }
}

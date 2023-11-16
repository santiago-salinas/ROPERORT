import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'RoperORT';

  slogans: string[] = [
    "Descubre tu estilo con ropa de calidad, solo en RoperORT",
    "Moda que te hace sentir ORTiginal en RoperORT",
    "Viste con confianza, viste con RoperORT",
    "Tu estilo, tu elección, tu Ropero en RoperORT", // :D
    "Moda que refleja tu personalidad, solo en RoperORT",
    "Estilo y comodidad se encuentran en RoperORT",
    "Viste con confianza y estilo, elige RoperORT",
    "Tu moda, tu elección, tu Ropero en RoperORT",
    "Calidad y tendencia se unen en RoperORT",
    "Elige calidad, elige RoperORT",
    "Donde la moda y la calidad son una sola, RoperORT",
    "Descubre la moda que te hace destacar, RoperORT te espera",
  ];

  randomSlogan: string = "";

  constructor() {
    this.generateRandomSlogan();
  }

  generateRandomSlogan() {
    const randomIndex = Math.floor(Math.random() * this.slogans.length);
    this.randomSlogan = this.slogans[randomIndex];
  }
}

export class Product {
  constructor() {
    this.brand = {
      name: '',
    };
    this.category = {
      name: '',
    };
    this.colours = [
      {
        name: 'placeholder',
      },
    ];
  }

  addColour(colour: string) {
    if(colour == '') return;
    this.colours.push({
      name: colour,
    });
  }


  id: number = -1;
  name: string = '';
  exclude: boolean = false;
  priceUYU: number = 0;
  description: string = '';
  stock: number = 0;
  brand!: {
    name: string;
  };
  category!: {
    name: string;
  };
  colours!: [
    {
      name: string;
    }
  ];
}



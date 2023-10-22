export class Product {
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



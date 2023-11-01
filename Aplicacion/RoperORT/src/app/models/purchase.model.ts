export class Purchase{
    id: string = "";
    email: string = "";
    paymentMethod: string = "";
    paymentMethodId: string = "";
    date: string = "";
    products: ProductDTO[] = [];
    price: number = 0;
    discountedPrice: number = 0;
    promo: string = "";
}

export class ProductDTO{
    name: string = "";
    price: number = 0;
    quantity: number = 0;
}
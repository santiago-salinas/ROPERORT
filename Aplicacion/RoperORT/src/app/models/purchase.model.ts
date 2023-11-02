export class Purchase {
    id: number = 0;
    user: User = new User();
    cart: Cart = new Cart();
    date: string = "";
}

class User {
    email: string = "";
}

class Product {
    name: string = "";
    priceUYU: number = 0;
}
  
class CartProduct {
    product: Product = new Product();
    quantity: number = 0;
}
  
class AppliedPromo {
    name: string = "";
}
  
class Cart {
    products: CartProduct[] = [];
    priceUYU: number = 0;
    discountedPriceUYU: number = 0;
    appliedPromo: AppliedPromo = new AppliedPromo();
}
  

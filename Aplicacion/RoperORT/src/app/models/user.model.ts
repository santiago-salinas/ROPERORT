export class User{
    id: number = -1;
    email: string = "";
    password: string = "";
    token: string = "";
    address: string = "";
    roles?: [ { name: string } ]
}
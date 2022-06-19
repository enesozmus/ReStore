export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    brand: string;
    quantityInStock?: number;
    createdDate: Date;
}
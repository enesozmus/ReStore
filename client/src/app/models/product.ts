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

export interface ProductParams {
    orderBy: string;
    searchTerm?: string;
    colors: string[];
    brands: string[];
    pageNumber: number;
    pageSize: number;
}
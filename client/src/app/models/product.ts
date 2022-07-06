export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    brand?: string;
    color?: string;
    category?: string;
    quantityInStock?: number;
    createdDate: Date;
    updatedDate: Date;
}

export interface ProductParams {
    orderBy: string;
    searchTerm?: string;
    colors: string[];
    brands: string[];
    categories: string[];
    pageNumber: number;
    pageSize: number;
}
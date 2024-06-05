export interface PropertyAddress {
    id: string;
    street: string;
    city: string;
    state: string;
    country: string;
    zipCode: string;
    propertyId: string;
}

export interface Favorites {
    id: string;
    userId: string;
    user: string;
    propertyId: string;
}

export interface Property {
    id: string;
    title: string;
    description: string;
    price: number;
    roomCount: number;
    status: string;
    images: string[];
    userId: string;
    user: {
        id: string;
        userName: string;
        email: string;
        role: number;
        properties: any[];
        favorites: Favorites[],
        rentals: any[]
    }
    address: PropertyAddress;
    favorites: Favorites[];
    rentals: any[];
}

import { Photo } from "./photo.model";

        export interface SubProduct {
        name: string;
        typeId?: any;
        type?: any;
        categoryId?: any;
        category: string;
        productId: number;
        product: string;
        discontinued: boolean;
        unitInStock: number;
        unitPrice: number;
        quantityPerUnite: number;
        unitsOnOrder: number;
        reorderLevel: number;
        photoUrl: string;
        photos: Photo[];
        }


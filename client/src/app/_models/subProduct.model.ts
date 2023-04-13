import { Photo } from "./photo.model";
import { SubProductSN } from "./subProductSN.model";

        export interface SubProduct {
        name: string;
        typeId?: any;
        type?: any;
        categoryId?: any;
        category: string;
        productId: number;
        quantity: number;
        product: string;
        discontinued: boolean;
        unitInStock: number;
        unitPrice: number;
        quantityPerUnite: number;
        unitsOnOrder: number;
        reorderLevel: number;
        photoUrl: string;
        photos: Photo[];
        withSerialNumber:boolean;
        id:number;
        subProductSNs:SubProductSN[]
        }


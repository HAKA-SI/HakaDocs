import { Customer } from "./customer.model";


export interface PhysicalBasket {
    customer: Customer;
    details: PhysicalBasketDetails;
    products: SubProduct[];
    subTotal: number;
    total: number;
}


interface PhysicalBasketDetails {
    orderDate: Date,
    orderNum: string,
    observation: string,
    invoiceSendingType: number[],
    delivered: boolean,
    paimentType: number,
    amountPaid: number,
}

interface SubProduct {
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
    discount: number;
    subTotal: number;
    total: number;
    photoUrl: string;
    // photos: Photo[];
    withSerialNumber: boolean;
    id: number;
    subProductSNs: SubProductSN[]
}


interface SubProductSN {
    id: number;
    subProductId: number;
    storeId: number;
    subProduct: SubProduct;
    sn: SubProduct;
    quantity: number;
    discount: number;
    subTotal: number;
    total: number;
}
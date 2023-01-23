import { Store } from "./store.model";
import { SubProduct } from "./subProduct.model";

export interface InvenOp{
    id: number;
        insertDate: Date;
        updateDate: Date;
        inventOpTypeId: number;
        opDate: Date;
        fromStoreId?: any;
        fromEmployeeId?: any;
        toStoreId: number;
        toEmployeeId?: any;   
        formNum: string;
        status: number;
        insertUserId: number;
        fromEmployee?: any;
        fromStore?: any;
        inventOpType: string;
        toEmployee?: any;
        toStore: Store;
        subProductId: number;
        subProduct: SubProduct;
        quantity: number;
}
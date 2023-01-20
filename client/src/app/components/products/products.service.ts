import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private baseUrl = environment.apiUrl+'Products/';

  
  constructor(private http: HttpClient, private router: Router) {}

  createCategory(clientId:number,categoryName:string,productGroupId:number){
    return this.http.post(this.baseUrl+"CreateCategory/"+clientId+'/'+categoryName+'/'+productGroupId,{});
  }

  categoryList(clientId:number,productGroupId:number) {
    return this.http.get(this.baseUrl+"CategoryList/"+productGroupId+'/'+clientId);
  }

  categoryWithDetailsList(clientId:number,productGroupId:number) {
    return this.http.get(this.baseUrl+"CategoryListWithDetails/"+productGroupId+'/'+clientId);
  }

  editCategory(clientId:number,categoryId:number,categoryName:string){
    return this.http.put(this.baseUrl+"EditCategory/"+clientId+'/'+categoryId+'/'+categoryName,{});
  }

  deleteCategory(clientId:number,categoryId:number) {
    return this.http.delete(this.baseUrl+"DeleteCategory/"+clientId+'/'+categoryId);
  }

  createProduct(clientId:number,categoryId:number,productName:string){
    return this.http.post(this.baseUrl+"CreateProduct/"+clientId+'/'+categoryId+'/'+productName,{});
  }

  editProduct(clientId:number,productId:number,categoryId:number,productName:string){
    return this.http.put(this.baseUrl+"EditProduct/"+productId+'/'+clientId+'/'+categoryId+'/'+productName,{});
  }

  deleteProduct(clientId:number,productId:number) {
    return this.http.delete(this.baseUrl+"DeleteProduct/"+productId+'/'+clientId);
  }

  getProductsWithDetailsList(clientId:number,productGroupId:number){
    return this.http.get(this.baseUrl+"ProductsWithDetails/"+productGroupId+'/'+clientId)
  }
  productList(clientId:number,productGroupId:number){
    return this.http.get(this.baseUrl+"ProductList/"+productGroupId+'/'+clientId)
  }

  createSubProduct(clientId:number,productGroupId,productData:any) {
    return this.http.post(this.baseUrl+'CreateSubProduct/'+productGroupId+''+clientId,productData);
  }

  editSubProduct(clientId:number,subPorductId:number,photoEdited:boolean,productData:any) {
    return this.http.post(this.baseUrl+'EditSubProduct/'+subPorductId+'/'+photoEdited+'/'+clientId,productData);
  }

  getSubProducts(productGroupId,clientId:number){
    return this.http.get(this.baseUrl+"SubProductList/"+productGroupId+'/'+clientId)
  }

  getSubProduct(subPorductId,clientId:number){
    return this.http.get(this.baseUrl+"SubProduct/"+subPorductId+'/'+clientId)
  }

  createSubProductsWithoutSN(clientId:number,productData:any) {
    return this.http.post(this.baseUrl+'CreateSubProductsNoSN/'+clientId,productData);
  }

  createSubProductsWithSN(clientId:number,productData:any) {
    return this.http.post(this.baseUrl+'CreateSubProductsSN/'+clientId,productData);
  } 

  subProductSNBySubProductId(clientId:number,subPorductId:number) {
    return this.http.get(this.baseUrl+"SubProductSnBySubProductId/"+clientId+'/'+subPorductId)
  }

}

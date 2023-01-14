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

  createCategory(clientId:number,categoryName:string){
    return this.http.post(this.baseUrl+"CreateCategory/"+clientId+'/'+categoryName,{});
  }

  categoryList(clientId:number) {
    return this.http.get(this.baseUrl+"CategoryList/"+clientId);
  }

  categoryWithDetailsList(clientId:number) {
    return this.http.get(this.baseUrl+"CategoryListWithDetails/"+clientId);
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

  getProductsWithDetailsList(clientId:number){
    return this.http.get(this.baseUrl+"ProductsWithDetails/"+clientId)
  }
  getProducts(clientId:number){
    return this.http.get(this.baseUrl+"productLisr/"+clientId)
  }

}

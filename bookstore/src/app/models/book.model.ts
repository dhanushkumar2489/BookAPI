import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class Book {
    Id:number=0;
    Author :string='';
    Title:string='';
    Price:number=0;

    toStr() {
        return "Id = " + this.Id + ", Author = " + this.Author + ", Title = " + this.Title + ", Price = " + this.Price
      }
}

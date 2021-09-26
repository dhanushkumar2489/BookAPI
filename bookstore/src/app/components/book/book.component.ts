import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../../data.service';
import { Book } from '../../models/book.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  booksForm: FormGroup = new FormGroup({
    Id: new FormControl(),
    Author: new FormControl(''),
    Title: new FormControl(''),
    Price: new FormControl(0),
  });
  API_URL='http://localhost:6422/api/Book';
  books:Book[]=[];
  book: Book = new Book();
  initialFormValue = this.booksForm.value;
  constructor(private dataService:DataService,private router:Router){ }

  ngOnInit(): void {
    return this.getAllBooks();
  }
  getAllBooks()
  {
    this.dataService.getBooks().subscribe(data =>{
      this.books=data;
    });
  }

  handleViewBook(id:number)
  {
    this.dataService.getBooksById(id.toString()).subscribe((data:Book) =>{
      this.book=data;
      alert(
        "Id           : "+data.Id+"\n"+
        "Author    : "+data.Author+"\n"+
        "Title        : "+data.Title+"\n"+
        "Price        : "+data.Price+"\n"
        );
    })
  }
  handleChangeBook(id:string)
  {
    if(id=="AddBtn")
     {
       this.dataService.addBook(this.booksForm.value)
         .subscribe((res: Book) => {
           alert("\nRow Updated ");
           this.getAllBooks();
         });
    }
    
    else if(id=="updateBtn"){
      this.dataService.updateBook(this.booksForm.value.Id, this.booksForm.value)
      .subscribe((res: Book) => {
        res.toStr = new Book().toStr;
        alert("\nRow has Updated");
        this.getAllBooks();
      });
  }
    this.booksForm.reset(this.initialFormValue);
  }
  handleDeleteBook(id:number)
  {
    this.dataService.DeleteBook(id)
      .subscribe(() => {
        alert("\nRow with Id = " + id +" is Deleted");
        this.getAllBooks();
      });
  }
}


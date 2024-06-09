import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthserviceService } from './authservice.service';
import { Stock } from '../Models/stock';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  baseUrl: string = 'http://localhost:5038/api/';
  constructor(private http:HttpClient,private service:AuthserviceService) { }
  getStocks():Observable<Stock[]>{
    return this.http.get<Stock[]>(this.baseUrl+'Stocks',{ headers: this.service.getHeaders() });
  }
}

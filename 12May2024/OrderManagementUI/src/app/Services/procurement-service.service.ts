import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthserviceService } from './authservice.service';
import { Procurement } from '../Models/procurement';
import { Observable } from 'rxjs';
import { ProcurementDTO } from '../Models/procurement-details';

@Injectable({
  providedIn: 'root'
})
export class ProcurementServiceService {

  baseUrl: string = 'http://localhost:5038/api/';
  constructor(private http:HttpClient,private service:AuthserviceService) { }
  getProcurement():Observable<Procurement[]>{
    return this.http.get<Procurement[]>(this.baseUrl+'Procurements',{ headers: this.service.getHeaders() });
  }
  getProcurementById(id:number):Observable<Procurement>{
    return this.http.get<Procurement>(this.baseUrl+'Procurements/'+id,{ headers: this.service.getHeaders() });
  }
  postProcurement(data:ProcurementDTO):Observable<ProcurementDTO>{
    return this.http.post<ProcurementDTO>(this.baseUrl+'Procurements',data,{ headers: this.service.getHeaders() });
  }

  getItemUnitPrice(itemId: number): Observable<number> {
    return this.http.get<number>(`api/items/${itemId}/unitprice`);
  }
}

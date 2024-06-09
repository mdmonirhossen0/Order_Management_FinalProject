import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthserviceService } from './authservice.service';
import { ProcurementDetails } from '../Models/procurement-details';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProcurementDetailsService {

  baseUrl: string = 'http://localhost:5038/api/';
  constructor(private http:HttpClient,private service:AuthserviceService) { }
  getProcurementDetails():Observable<ProcurementDetails[]>{
    return this.http.get<ProcurementDetails[]>(this.baseUrl+'ProcurementDetails',{ headers: this.service.getHeaders() });
  }
  getProcurementDetailsById(id:number):Observable<ProcurementDetails>{
    return this.http.get<ProcurementDetails>(this.baseUrl+'ProcurementDetails/'+id,{ headers: this.service.getHeaders() });
  }
}

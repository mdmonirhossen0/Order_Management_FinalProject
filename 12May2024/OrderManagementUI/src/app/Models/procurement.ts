import { ProcurementDetails } from "./procurement-details";

export interface Procurement{
    procurementId:number,
    procurementDate:Date,
    requisitionDate:Date,
    amount:number,
    procurementDetails:ProcurementDetails[]

}
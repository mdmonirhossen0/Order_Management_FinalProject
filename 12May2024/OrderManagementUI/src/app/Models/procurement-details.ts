import { Item } from "./item";
import { Procurement } from "./procurement";
export interface ProcurementDetails{
    procurementDetailsId:number,
    procurementId:number,
    procurement:Procurement,
    procurementQuantity:number,
    itemId:number,
    item:Item
    itemUnitPrice:number,
    itemTotalPrice:number
}

export interface ProcurementDTO{
    proDate:Date,
    reqDate:Date,
    procurementDetails: string
}
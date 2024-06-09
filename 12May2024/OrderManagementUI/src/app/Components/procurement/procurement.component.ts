import { Component, OnInit, ViewChild } from '@angular/core';
import { ProcurementServiceService } from '../../Services/procurement-service.service';
import { RepositoryService } from '../../Services/repository.service';
import { Requisition } from '../../Models/requisition';
import {
  ProcurementDTO,
  ProcurementDetails,
} from '../../Models/procurement-details';
import { error } from 'console';
import { Item } from '../../Models/item';
import { ItemService } from '../../Services/item.service';
import { ProcurementDetailsService } from '../../Services/procurement-details.service';
import { response } from 'express';
import { MatTabGroup } from '@angular/material/tabs';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Procurement } from '../../Models/procurement';
import { setPdfFonts } from '../../PDFMaker/pdfUtils';

@Component({
  selector: 'app-procurement',
  templateUrl: './procurement.component.html',
  styleUrl: './procurement.component.css',
})
export class ProcurementComponent implements OnInit {
  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;

  constructor(
    private service: ProcurementServiceService,
    private reqService: RepositoryService,
    private itemService: ItemService,
    private procurementDetailService: ProcurementDetailsService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  requsitionList: Requisition[] = [];
  itemUnitPrice: number = 0;
  itemList: Item[] = [];
  procurementList: Procurement[] = [];
  procurementObj: ProcurementDTO = {
    proDate: new Date(),
    reqDate: new Date(),
    procurementDetails: '',
  };
  procurementItemListAdd: ProcurementDetails[] = []; //For add details
  procurementDetailList: ProcurementDetails[] = []; //For display
  procurementDetailsObj: ProcurementDetails = {
    procurementId: 0,
    itemId: 0,
    procurementQuantity:0,
    procurementDetailsId: 0,
    procurement: {
      procurementId: 0,
      procurementDate: new Date(),
      requisitionDate: new Date(),
      amount: 0,
      procurementDetails:[]
    },
    item: {
      itemId: 0,
      name: '',
      unit: '',
      type: '',
    },
    itemUnitPrice: 0,
    itemTotalPrice: 0,
  };

  ngOnInit(): void {
    this.reqService.getAllRequisitions().subscribe((x) => {
      (this.requsitionList = x),
        this.itemService.getItems().subscribe((i) => {
          this.itemList = i;
        });
      this.procurementDetailService.getProcurementDetails().subscribe((x) => {
        this.procurementDetailList = x;
        console.log(this.procurementDetailList);
      });
      this.getProcurement();
    });
  }
  getProcurement() {
    this.service.getProcurement().subscribe((x) => {
      this.procurementList = x;
      console.log(this.procurementList);
    });
  }

  getDistinctDates() {
    const dates = this.requsitionList.map((item) => item.requisitionDate);
    return [...new Set(dates)];
  }

  addProcurementDetails() {
    this.procurementItemListAdd.unshift(this.procurementDetailsObj);
    this.procurementDetailsObj = {
      procurementId: 0,
      itemId: 0,
      procurementQuantity:0,
      procurementDetailsId: 0,
      procurement: {
        procurementId: 0,
        procurementDate: new Date(),
        requisitionDate: new Date(),
        amount: 0,
        procurementDetails:[]
      },
      item: {
        itemId: 0,
        name: '',
        unit: '',
        type: '',
      },
      itemUnitPrice: 0,
      itemTotalPrice: 0,
    };
  }

  
  deleteProcurementDetails(del: ProcurementDetails, arr: any[]) {
    const obj = arr.findIndex((x) => x === del);
    if (obj > -1) {
      arr.splice(obj, 1);
    }
  }

  postProcurement() {
    const proDetailJson = JSON.stringify(this.procurementItemListAdd);
    const proDTO: ProcurementDTO = {
      proDate: this.procurementObj.proDate,
      reqDate: this.procurementObj.reqDate,
      procurementDetails: proDetailJson,
    };
    this.service.postProcurement(proDTO).subscribe({
      next: (response) => {
        console.log(response);
        this.procurementObj = {
          proDate: new Date(),
          reqDate: new Date(),
          procurementDetails: '',
        };
        this.getProcurement();
        this.tabGroup.selectedIndex = 1;
        this.showSnackbar("Procurement successful.");
      },
      error: (error) => {
        console.error('Error adding procurement.', error);
        this.showSnackbar('Failed to add procurement.');
      },
    });
  }

  showSnackbar(message: string) {
    this.snackBar.open(message, "Close", {
      duration: 3000
    });
  }

  generateInvoice(procurement: Procurement): void {
    if(this.procurementDetailList){
      const invoiceContent = [
        { text: 'Procurement', fontSize: 18, bold: true, alignment: 'center', margin: [0, 0, 0, 5] },
        { text: '7th-8th Floor, Fattah Plaza, 70 Green Rd, Dhaka 1205.', fontSize: 11, bold: false, alignment: 'center', margin: [0, 0, 0, 20] },
        { text: `ProcurementDate: ${procurement.procurementDate}`, fontSize: 11, bold: true },
        { text: `RequisitionDate: ${procurement.requisitionDate}`, fontSize: 11 },
        { canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595 - 2 * 40, y2: 5, lineWidth: 0.5 }] },
        {
          table: {
            widths: ['*', '*', '*', '*'],
            body: [
              ['ItemName','Quantity', 'Price', 'Total'],
              ...procurement.procurementDetails.map(detail => {
                const proDet = this.procurementDetailList.find(menu => menu.procurementId === detail.procurementId);
                const itemnamelist = this.itemList.find(menu => menu.itemId === detail.itemId);
                return [`${itemnamelist?.name}`,`${detail.procurementQuantity}`, `$${detail.itemUnitPrice.toFixed(2)}`, `$${detail.itemTotalPrice.toFixed(2)}`];
              })
            ]
          },
          layout: 'lightHorizontalLines'
        },
        {
          canvas: [
            {
              type: 'line',
              x1: 0, y1: 5,
              x2: 515, y2: 5,
              lineWidth: 1,
              lineColor: 'black',
              dash: { length: 5, space: 3 }  // Customizable lengths and spacing
            }
          ]
        },   
        {
          canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595 - 2 * 40, y2: 5, lineWidth: 1 }]
        },
        {
          table: {
            widths: ['*', '*', '*', '*'],
            body: [
              [
                { text: 'Grand Total:', bold: true, fontSize: 16, margin: [0, 0, 0, 20] },
                '',
                '',
                { text: `$${procurement.amount.toFixed(2)}`, bold: true, fontSize: 16 }
              ]
            ]
          },
          layout: 'lightHorizontalLines'
        },
        { text: 'Sign___________________', fontSize: 16, bold: true, alignment: 'left', margin: [0, 0, 0, 20] },
      ];
      const pdfMakeInstance = setPdfFonts();
      const documentDefinition = {
        content: invoiceContent
      };
      pdfMakeInstance.createPdf(documentDefinition as any).open();
    }
    }
    
}

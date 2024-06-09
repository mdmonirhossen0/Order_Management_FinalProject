import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { SaleHeader } from '../../Models/saleheader';
import { SaleheaderService } from '../../Services/saleheader.service';
import { SaleDetails } from '../../Models/sale-details';
import { RecipeService } from '../../Services/recipe.service';
import { MenuService } from '../../Services/menu.service';
import { DailyMenu } from '../../Models/daily-menu';
import { MatTabGroup } from '@angular/material/tabs';
import { Recipe } from '../../Models/recipe';
import { setPdfFonts } from '../../PDFMaker/pdfUtils';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { error, table } from 'console';


@Component({
  selector: 'app-sale-header',
  templateUrl: './sale-header.component.html',
  styleUrl: './sale-header.component.css'
})

export class SaleHeaderComponent implements OnInit {

  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;
  @Input() selectedDate: Date = new Date();
  filteredDailyMenuList: DailyMenu[] = [];
  saleHeaderList: SaleHeader[] = [];
  saleHeaderobj: SaleHeader = { id: 0, invoiceNumber: "", customerName: "", customerEmail: "", customerPhone: "", totalPrice: 0, vat: 0, totalBill: 0, saleDate: new Date(), saleDetails: [] }
  saleDetailList: SaleDetails[] = [];
  saleDetailsObj: SaleDetails = { id: 0, dailyMenuId: 0, saleHeaderId: 0, quantity: 0, saleHeader: null, dailyMenu: null }
  dailyMenuList: DailyMenu[] = [];
  editing: boolean[] = [];
  today: string = new Date().toISOString().split('T')[0]; // Calculate today's date
  dailyMenuDate: Date[] = [];

  constructor(
    private service: SaleheaderService,
    private dailymenuservice: MenuService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.service.getSales().subscribe(x => {
      this.saleHeaderList = x;
      console.log(x);
    })
    this.dailymenuservice.getDailyMenu().subscribe(x => {
      this.dailyMenuList=x;
    });
  }
  filterDailyMenuList(): void {
    const selectedDate = new Date(this.saleHeaderobj.saleDate);
    this.filteredDailyMenuList = this.dailyMenuList.filter(menu => {
      const menuDate = new Date(menu.dailyMenuDate);
      if (menuDate.toDateString() !== selectedDate.toDateString()) {
        return false; 
      }
      const soldQuantity = this.saleHeaderList
        .filter(sale => new Date(sale.saleDate).toDateString() === selectedDate.toDateString())
        .flatMap(sale => sale.saleDetails)
        .filter(detail => detail.dailyMenuId === menu.dailyMenuId)
        .reduce((sum, detail) => sum + detail.quantity, 0);
  
      return menu.cookedQuantity > soldQuantity; 
    });
  }
  
  addMenus() {
    if (this.saleDetailsObj.quantity != 0) {
      const strExpObj = JSON.stringify(this.saleDetailsObj);
      const obj = JSON.parse(strExpObj);
      this.saleDetailList.unshift(obj);
      console.log(this.saleDetailList);
      this.saleDetailsObj = { id: 0, dailyMenuId: 0, saleHeaderId: 0, quantity: 0, saleHeader: null, dailyMenu: null }
    }
  }

  deleteDetail(exp: SaleDetails, arry: any[]) {
    const objWithTitle = arry.findIndex((obj) => obj === exp);
    if (objWithTitle > -1) {
      arry.splice(objWithTitle, 1);
    }
  }
  addSales(): void {
    this.saleHeaderobj.saleDetails = this.saleDetailList;
    this.service.postSales(this.saleHeaderobj).subscribe({
      next:(res)=>{
        console.log(this.saleHeaderobj);
        this.saleHeaderobj = { id: 0, invoiceNumber: "", customerName: "", customerEmail: "", customerPhone: "", totalPrice: 0, vat: 0, totalBill: 0, saleDate: new Date(), saleDetails: [] };
        this.service.getSales().subscribe(x => {
          this.saleHeaderList = x;
          console.log(x);
          this.generateInvoice(this.saleHeaderList[this.saleHeaderList.length - 1]);
        })
        this.dailymenuservice.getDailyMenu().subscribe(x => {
          this.dailyMenuList = x;
        });
        this.tabGroup.selectedIndex = 1;
        this.showSnackbar('Sale completed successfully.');
      }
    ,
      error: (error)=> {
        console.error('Error on making a sale.', error);
        this.showSnackbar('Failed to sale.');
      }
  });
  }
  showSnackbar(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 3000
    });
  }

  deleteSales(id: number) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '300px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.service.deleteSales(id).subscribe(x => {
          console.log("Deleted");
          this.service.getSales().subscribe(updatedData => {
            this.saleHeaderList = updatedData;
          });
        })
      }
    });
  }

  generateInvoice(saleHeader: SaleHeader): void {
    const invoiceContent = [
      { text: 'INVOICE', fontSize: 18, bold: true, alignment: 'center', margin: [0, 0, 0, 5] },
      { text: '7th-8th Floor, Fattah Plaza, 70 Green Rd, Dhaka 1205.', fontSize: 11, bold: false, alignment: 'center', margin: [0, 0, 0, 20] },
      { text: `Customer: ${saleHeader.customerName}`, fontSize: 11, bold: true },
      { text: `Email: ${saleHeader.customerEmail}`, fontSize: 11 },
      { text: `Phone: ${saleHeader.customerPhone}`, fontSize: 11 },
      { text: `InvoiceNumber: ${saleHeader.invoiceNumber}`, fontSize: 11 },
      { text: `Date: ${this.saleHeaderobj.saleDate.toString()}`, fontSize: 11 },
      { canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595 - 2 * 40, y2: 5, lineWidth: 0.5 }] },
      {
        table: {
          widths: ['*', '*', '*', '*'],
          body: [
            ['Item', 'Quantity', 'Price', 'Total'],
            ...saleHeader.saleDetails.map(detail => {
              const dailyMenu = this.dailyMenuList.find(menu => menu.dailyMenuId === detail.dailyMenuId);
              const itemPrice = dailyMenu ? dailyMenu.price : 0;
              const totalPrice = itemPrice * detail.quantity;
              return [`${dailyMenu ? dailyMenu.recipe?.recipeName : ''}`, `${detail.quantity}`, `$${itemPrice.toFixed(2)}`, `$${totalPrice.toFixed(2)}`];
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
        table: {
          widths: ['*', '*', '*', '*'],
          body: [
            [
              { text: 'Sub Total:', bold: true, fontSize: 12 },
              '',
              '',
              { text: `$${saleHeader.totalPrice.toFixed(2)}`, bold: true, fontSize: 12 }
            ]
          ]
        },
        layout: 'lightHorizontalLines'
      },
      {
        table: {
          widths: ['*', '*', '*', '*'],
          body: [
            [
              { text: 'VAT:', bold: true, fontSize: 12 },
              '',
              '',
              { text: `$${saleHeader.vat.toFixed(2)}`, bold: true, fontSize: 12 }
            ]
          ]
        },
        layout: 'lightHorizontalLines'
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
              { text: `$${saleHeader.totalBill.toFixed(2)}`, bold: true, fontSize: 16 }
            ]
          ]
        },
        layout: 'lightHorizontalLines'
      },
      { text: 'Thank you for visiting us', fontSize: 16, bold: true, alignment: 'center', margin: [0, 0, 0, 20] },
    ];
    const pdfMakeInstance = setPdfFonts();
    const documentDefinition = {
      content: invoiceContent
    };
    pdfMakeInstance.createPdf(documentDefinition as any).open();
  }
  isPastDate(date: string): boolean {
    let today = new Date();
    today.setHours(0, 0, 0, 0);
    let itemDate = new Date(date);
    return itemDate < today;
  }
  isFutureDate(date: string): boolean {
    let today = new Date();
    today.setHours(0, 0, 0, 0);
    let itemDate = new Date(date);
    return itemDate > today;
  }
}
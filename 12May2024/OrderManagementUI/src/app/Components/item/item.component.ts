import { Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { Item } from '../../Models/item';
import { ItemService } from '../../Services/item.service';
import { Router } from '@angular/router';
import { response } from 'express';
import { MatTabGroup } from '@angular/material/tabs';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PagingConfig } from '../../Models/paging-config';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrl: './item.component.css',
})
export class ItemComponent implements OnInit {

  items: Item[] = [];
  newItem: Item = { itemId: 0, name: '', unit: '', type: ''};
  editingItem: boolean[] = [];

  currentPage: number = 1;//paging
  itemsPerPage: number = 5;//paging
  totalItems: number = 0;//paging
  tableSize: number[] = [5, 10, 15, 20];//paging
  pagingConfig: PagingConfig = {} as PagingConfig;//paging
  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;//paging

  constructor(
    private itemService: ItemService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
  ) {//paging
    this.getItems();

    this.pagingConfig = {
      itemsPerPage: this.itemsPerPage,
      currentPage: this.currentPage,
      totalItems: this.totalItems
    }
    //paging
  }

  ngOnInit(): void {
    this.getItems();
  }

  getItems() {
    this.itemService.getItems().subscribe((items) => (this.items = items));
    this.pagingConfig.totalItems = this.items.length;//paging
  }

  //paging
  onTableDataChange(event:any){
    this.pagingConfig.currentPage  = event;
    this.getItems();
  }
  //paging
  onTableSizeChange(event:any): void {
    this.pagingConfig.itemsPerPage = event.target.value;
    this.pagingConfig.currentPage = 1;
    this.getItems();
  }

  addItem(): void {
    this.itemService.postItem(this.newItem).subscribe({
      next: (item) => {
        console.log(item);
        this.newItem = { itemId: 0, name: '', unit: '', type: ''};
        this.getItems();
        this.tabGroup.selectedIndex = 1;
        this.showSnackbar('Item added successfully!');
      },
      error: (error) => {
        console.error('Error adding item', error);
        this.showSnackbar('Failed to add item.');
      },
    });
  }

  showSnackbar(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
    });
  }

  updateItem(index: number, item: Item) {
    this.itemService.updateItem(item.itemId, item).subscribe({
      next: (response) => {
        console.log(response);
        this.editingItem[index] = false;
        this.itemService.getItems().subscribe((x) => {
          this.items = x;
        });
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  cancelEdit(index: number): void {
    this.editingItem[index] = false;
  }

  deleteItem(itemId: number): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '300px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.itemService.deleteItem(itemId).subscribe(() => {
          console.log('Deleted');
          this.itemService.getItems().subscribe((x) => {
            this.items = x;
          });
        });
      }
    });
  }
}

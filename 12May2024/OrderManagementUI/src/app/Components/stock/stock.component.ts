import { Component, OnInit } from '@angular/core';
import { Stock } from '../../Models/stock';
import { Item } from '../../Models/item';
import { StockService } from '../../Services/stock.service';
import { ItemService } from '../../Services/item.service';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrl: './stock.component.css'
})
export class StockComponent implements OnInit {
  stockList: Stock[] = [];
  itemList: Item[] = [];
  constructor(private service: StockService, private itemService: ItemService){}

  ngOnInit(): void {
    this.service.getStocks().subscribe(x => {
      this.stockList = x;
    })
    this.itemService.getItems().subscribe(x => {
      this.itemList = x;
    })
  }

  
}

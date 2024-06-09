import { Component, OnInit } from '@angular/core';
import { Chart } from 'angular-highcharts';
@Component({
  selector: 'app-right-widget',
  templateUrl: './right-widget.component.html',
  styleUrl: './right-widget.component.css'
})
export class RightWidgetComponent implements OnInit {
  constructor(){}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  getPaymentTypeCount(){

  }
  chart = new Chart({
    chart: {
      type: 'pie',
      height: 325
    },
    title: {
      text: 'Expenditure'
    },
    xAxis: {
      categories: [
        'Salaries',
        'Accesories',
        'Cosmetics',
        'Clothes',
        'Others',
      ]
    },
    yAxis: {
      title: {
        text: 'Revenue in %'
      }
    },
    series: [
     {
      type: 'pie',
      data: [
        {
          name: 'Salaries',
          y: 41.0,
          color: '#044342',
        },
        {
          name: 'Accesories',
          y: 33.8,
          color: '#7e0505',
        },
        {
          name: 'Rent',
          y: 6.5,
          color: '#ed9e20',
        },
        {
          name: 'Furnitures',
          y: 15.2,
          color: '#6920fb',
        },
        {
          name: 'Others',
          y: 3.5,
          color: '#121212',
        },
      ]
     }
    ],
    credits: {
      enabled: false
    }
  })

}

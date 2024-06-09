import { Component, OnInit, ViewChild } from '@angular/core';
import { DailyMenu, DailyMenuDTO, DailyMenuDTOAfter } from '../../Models/daily-menu';
import { MenuService } from '../../Services/menu.service';
import { Recipe } from '../../Models/recipe';
import { RecipeService } from '../../Services/recipe.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { PatchDialogComponent } from '../patch-dialog/patch-dialog.component';
import { MatTabGroup } from '@angular/material/tabs';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-daily-menu',
  templateUrl: './daily-menu.component.html',
  styleUrls: ['./daily-menu.component.css']
})
export class DailyMenuComponent implements OnInit {

  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;

  dailyMenuList: DailyMenu[] = [];
  dailyMenuObj: DailyMenuDTO = { dailyMenuDate: new Date(), demandQuantity: 0, recipeId: 0, price: 0 }
  recipes: Recipe[] = [];
  editing: boolean[] = [];

  constructor(
    private service: MenuService,
    private recipeService: RecipeService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.service.getDailyMenu().subscribe(x => {
      this.dailyMenuList = x;
    });
    this.recipeService.getRecipes().subscribe(x => {
      this.recipes = x;
    });
  }

  addDailyMenu() {
    this.service.postDailyMenu(this.dailyMenuObj).subscribe({
      next:(x)=>{
        console.log(x);
        this.dailyMenuObj = { dailyMenuDate: new Date(), demandQuantity: 0, recipeId: 0, price: 0 };
        this.updateDailyMenuList();
        this.tabGroup.selectedIndex = 1;
        this.showSnackbar('Menu added successfully.');
      }  
    , error:(error) => {
      console.error('Error adding menu', error);
      this.showSnackbar('Failed to add menu.');
   } });
  }

  showSnackbar(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 3000
    });
  }

  updateDailyMenu(index: number, dailyMenu: DailyMenu) {
    this.service.putDailyMenu(dailyMenu.dailyMenuId, dailyMenu).subscribe({
      next: response => {
        console.log(response);
        this.editing[index] = false;
        this.updateDailyMenuList();
      },
      error: error => {
        console.log(error);
      }
    });
  }

  cancelEdit(index: number): void {
    this.editing[index] = false;
  }

  deleteMenu(id: number) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '300px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.service.DeleteDailyMenu(id).subscribe(x => {
          console.log("Deleted");
          this.updateDailyMenuList();
        });
      }
    });
  }

  updateDailyMenuList() {
    this.service.getDailyMenu().subscribe(updatedData => {
      this.dailyMenuList = updatedData;
    });
  }

  openPatchDialog(item: DailyMenu) {
    const dialogRef = this.dialog.open(PatchDialogComponent, {
      width: '250px',
      data: {
        item: item,
        previousCookedQuantity: item.cookedQuantity 
      }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const newCookedQuantity = result.cookedQuantity;
        if (newCookedQuantity > item.cookedQuantity) {
          this.patchDailyMenu(item.dailyMenuId, newCookedQuantity);
        } else {
          this.showSnackbar('The new cooked quantity must be greater than the previous value.');
        }
      }
    });
  }

  // patchDailyMenu(dailyMenuId: number, cookedQuantity: number) {
  //   const patchData: DailyMenuDTOAfter = { cookedQuantity: cookedQuantity };
  //   this.service.patchDailyMenu(dailyMenuId, patchData).subscribe(x => {
  //     this.updateDailyMenuList();
  //   });
  // }
  patchDailyMenu(dailyMenuId: number, cookedQuantity: number) {
    const patchData: DailyMenuDTOAfter = { cookedQuantity: cookedQuantity };
    this.service.patchDailyMenu(dailyMenuId, patchData).pipe(
      catchError(this.handleError)
    ).subscribe(
      data => {
        // Handle the response here
        console.log('Patch successful');
        this.updateDailyMenuList();
      },
      error => {
        // This is where you can handle the error and show it to the user
        console.error(error);
        this.showSnackbar(error);
      }
    );
  }
  
  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
    }
    return new Observable(observer => {
      observer.error('Stock is low.Please get more stock');
    });
  }
  
}

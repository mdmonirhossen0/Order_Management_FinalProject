import { Component, OnInit, ViewChild } from '@angular/core';
import { RecipeService } from '../../Services/recipe.service';
import { Recipe, RecipeDTO } from '../../Models/recipe';
import { RecipeItems } from '../../Models/recipe-items';
import { Item } from '../../Models/item';
import { ItemService } from '../../Services/item.service';
import { MatTabGroup } from '@angular/material/tabs';
import { RecipeItemService } from '../../Services/recipe-item.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.css'
})
export class RecipeComponent implements OnInit {

  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;

  recipeList: Recipe[] = [];
  recipeitemList: RecipeItems[] = [];
  recipeItemListAdd: RecipeItems[] = []
  recipeItemObj: RecipeItems = { recipeItemId: 0, recipeId: 0, itemId: 0, recipe: { recipeId: 0, recipeName: "", recipeItems: [] }, item: { itemId: 0, name: "", unit: "", type: "" }, quantity: 0 }
  recipeObj: RecipeDTO = { recipeName: "", recipeItems: "" }
  itemList: Item[] = [];

  constructor(
    private service: RecipeService, 
    private itemService: ItemService, 
    private recipeItemService: RecipeItemService, 
    private dialog: MatDialog,
    private snackBar: MatSnackBar) { 

    }

  ngOnInit(): void {
    this.service.getRecipes().subscribe(x => {
      this.recipeList = x,
        this.itemService.getItems().subscribe(z => {
          this.itemList = z;
        })
      this.recipeItemService.getRecipeItems().subscribe(r => {
        this.recipeitemList = r;
        console.log(this.recipeitemList);
      })
    });
  }

  addRecipeItems() {
    this.recipeItemListAdd.unshift(this.recipeItemObj);
    this.recipeItemObj = { recipeItemId: 0, recipeId: 0, itemId: 0, recipe: { recipeId: 0, recipeName: "", recipeItems: [] }, item: { itemId: 0, name: "", unit: "", type: "" }, quantity: 0 };

  }

  deleteDetail(exp: RecipeItems, arry: any[]) {
    const objWithTitle = arry.findIndex((obj) => obj === exp);
    if (objWithTitle > -1) {
      arry.splice(objWithTitle, 1);
    }
  }

  postRecipe() {
    const recipeItemsJson = JSON.stringify(this.recipeItemListAdd);
    const recipeDTO: RecipeDTO = {
      recipeName: this.recipeObj.recipeName,
      recipeItems: recipeItemsJson
    };
    this.service.postRecipe(recipeDTO).subscribe({
      next: (response) => {
        console.log(response);
        this.recipeObj = { recipeName: "", recipeItems: "" };
        this.service.getRecipes().subscribe(x => {
          this.recipeList = x,
            this.itemService.getItems().subscribe(z => {
              this.itemList = z;
            })
          this.recipeItemService.getRecipeItems().subscribe(r => {
            this.recipeitemList = r;
            console.log(this.recipeitemList);
          })
        });
        this.tabGroup.selectedIndex = 1;
        this.showSnackbar('Recipe added successfully.');
      },
      error: (error) => {
        console.error('Error adding recipe.',error);
        this.showSnackbar('Failed to add recipe.');
      }
    });
  }
  showSnackbar(message: string) {
    this.snackBar.open(message, 'Close',{
      duration: 3000
    });
  }

  deleteRecipe(recipe: Recipe): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '300px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.service.deleteRecipe(recipe.recipeId).subscribe({
          next: (response) => {
            console.log(response);
            this.service.getRecipes().subscribe(x => {
              this.recipeList = x,
                this.itemService.getItems().subscribe(z => {
                  this.itemList = z;
                })
              this.recipeItemService.getRecipeItems().subscribe(r => {
                this.recipeitemList = r;
                console.log(this.recipeitemList);
              })
            });
          },
          error: (error) => {
            console.error(error);
          }
        });
      }
    });
  }

}




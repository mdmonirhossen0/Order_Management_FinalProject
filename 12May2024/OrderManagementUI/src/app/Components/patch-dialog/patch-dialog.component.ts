import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DailyMenu } from '../../Models/daily-menu';

export interface DialogData {
  item: DailyMenu;
  previousCookedQuantity: number;
}

@Component({
  selector: 'app-patch-dialog',
  templateUrl: './patch-dialog.component.html'
})
export class PatchDialogComponent {
  cookedQuantity: number = 0;
  previousCookedQuantity: number;

  constructor(
    public dialogRef: MatDialogRef<PatchDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    this.cookedQuantity = data.item.cookedQuantity;
    this.previousCookedQuantity = data.previousCookedQuantity;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onPatchClick(): void {
    if (this.cookedQuantity > this.previousCookedQuantity) {
      this.dialogRef.close({ cookedQuantity: this.cookedQuantity });
    } else {
      console.log('The new cooked quantity must be greater than the previous value.');
    }
  }
}
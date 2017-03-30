import { Component } from '@angular/core';
import { MdDialogRef } from '@angular/material';

@Component({
  selector: 'activity-dialog',
  templateUrl: 'activityDialog.component.html',
})
export class ActivityDialog {
  public dialogName: string;
  public id: number;
  public name: string;
  public units: number;
  public safeProductivityRate: number;
  public aggressiveProductivityRate: number;
  public duration: number;
  public aggressiveDuration: number;
  public inputProdRate: boolean;
  public startToFinish: number;
  public unitDelta: number;
  public durationFunction: number;
  public durationFunctions: Array<Object>;
  public activities: Array<Object>;
  public activitySelected: Array<number>;
  public processSelected: string;
  public section: string;
  public processes: Array<Object>;

  constructor(public dialogRef: MdDialogRef<ActivityDialog>) {
  }
}

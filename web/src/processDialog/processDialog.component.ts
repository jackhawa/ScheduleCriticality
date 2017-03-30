import { Component } from '@angular/core';
import { MdDialogRef } from '@angular/material';

@Component({
  selector: 'process-dialog',
  templateUrl: 'processDialog.component.html',
})
export class ProcessDialog {
  public dialogName: string;
  public id: number;
  public name: string;

  constructor(public dialogRef: MdDialogRef<ProcessDialog>) {
  }
}

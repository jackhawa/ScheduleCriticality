import { Component } from '@angular/core';
import { MdDialogRef } from '@angular/material';

@Component({
    selector: 'error-dialog',
    templateUrl: 'errorDialog.component.html',
})
export class ErrorDialog {
    public errorMessage: string;
    constructor(public dialogRef: MdDialogRef<ErrorDialog>) {
    }
}
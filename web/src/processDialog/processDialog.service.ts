import { Observable } from 'rxjs/Rx';
import { ProcessDialog } from './processDialog.component';
import { MdDialogRef, MdDialog, MdDialogConfig } from '@angular/material';
import { Injectable, ViewContainerRef } from '@angular/core';
import { Process, HttpService } from '../app/app.httpService';

@Injectable()
export class ProcessDialogService {

    constructor(private dialog: MdDialog,
        private httpService: HttpService) { }

    public confirm(process: Process, viewContainerRef: ViewContainerRef): Observable<ProcessDialog> {

        let dialogRef: MdDialogRef<ProcessDialog>;
        let config = new MdDialogConfig();
        config.viewContainerRef = viewContainerRef;

        dialogRef = this.dialog.open(ProcessDialog, config);
        dialogRef.componentInstance.dialogName = 'Add';

        if (process) {
            dialogRef.componentInstance.dialogName = 'Update';
            dialogRef.componentInstance.id = process.id;
            dialogRef.componentInstance.name = process.name;
        }

        return dialogRef.afterClosed();
    }
}

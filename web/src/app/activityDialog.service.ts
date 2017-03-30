import { Observable } from 'rxjs/Rx';
import { ActivityDialog } from './activityDialog.component';
import { MdDialogRef, MdDialog, MdDialogConfig } from '@angular/material';
import { Injectable, ViewContainerRef } from '@angular/core';
import { Activity, Process, HttpService } from './app.httpService';

@Injectable()
export class ActivityDialogService {

    constructor(private dialog: MdDialog,
        private httpService: HttpService) { }

    public confirm(activity: Activity, viewContainerRef: ViewContainerRef, section: string): Observable<ActivityDialog> {

        let dialogRef: MdDialogRef<ActivityDialog>;
        let config = new MdDialogConfig();
        config.viewContainerRef = viewContainerRef;

        dialogRef = this.dialog.open(ActivityDialog, config);

        dialogRef.componentInstance.durationFunctions = [
            { key: 0, value: 'MAX' },
            { key: 1, value: 'NONE' }];
        dialogRef.componentInstance.durationFunction = 1;
        dialogRef.componentInstance.duration = 10;
        dialogRef.componentInstance.aggressiveDuration = 5;
        dialogRef.componentInstance.inputProdRate = true;
        dialogRef.componentInstance.activitySelected = [,];
        dialogRef.componentInstance.dialogName = 'Add';
        dialogRef.componentInstance.units = 10;
        dialogRef.componentInstance.safeProductivityRate = 1;
        dialogRef.componentInstance.aggressiveProductivityRate = 1;
        dialogRef.componentInstance.startToFinish = 0;
        dialogRef.componentInstance.unitDelta = 0;
        this.httpService.getActivities()
            .subscribe((activities) => {
                dialogRef.componentInstance.activities = activities
                    .filter(a => a.section === section)
                    .map(p =>
                    { return { key: p.id, value: p.name }; });
                dialogRef.componentInstance.activities.push({ key: ' ', value: 'NONE' });
                if (activity) {
                    dialogRef.componentInstance.activitySelected = activity.dependencies.split(',')
                        .map(d => {
                            if (d !== "") return +d;
                            else return null;
                        });
                }
            });

        let processObs = this.httpService.getProcesses()
            .subscribe((processes) => {
                dialogRef.componentInstance.processes = processes
                    .map(p => { return { key: p.id, value: p.name }; });

                if (activity) {
                    dialogRef.componentInstance.processSelected = activity.processId;
                }
            });

        if (activity) {
            dialogRef.componentInstance.dialogName = 'Update';
            dialogRef.componentInstance.id = activity.id;
            dialogRef.componentInstance.name = activity.name;
            dialogRef.componentInstance.units = activity.units;
            dialogRef.componentInstance.safeProductivityRate = activity.safeProductivityRate;
            dialogRef.componentInstance.aggressiveProductivityRate = activity.aggressiveProductivityRate;
            dialogRef.componentInstance.duration = activity.duration;
            dialogRef.componentInstance.aggressiveDuration = activity.aggressiveDuration;
            dialogRef.componentInstance.inputProdRate = activity.inputProdRate;
            dialogRef.componentInstance.startToFinish = activity.startToFinish;
            dialogRef.componentInstance.unitDelta = activity.unitDelta;
            dialogRef.componentInstance.durationFunction = activity.durationFunction;
            dialogRef.componentInstance.name = activity.name;
            dialogRef.componentInstance.section = activity.section;
        }

        return dialogRef.afterClosed();
    }
}

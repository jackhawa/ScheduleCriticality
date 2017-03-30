import { Component, ViewContainerRef } from '@angular/core';
import { MdDialog } from '@angular/material';
import { Activity, Process, HttpService } from './app.httpService';
import { ActivityDialogService } from './activityDialog.service';
import { ProcessDialogService } from '../processDialog/processDialog.service';
import { ActivityDialog } from './activityDialog.component';
import { ProcessDialog } from '../processDialog/processDialog.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [HttpService]
})
export class AppComponent {
    public result: any;
    errorMessage: string;
    activities: Activity[];
    downwardActivities: Activity[];
    processes: Process[];
    graphConfig: {};
    editing = {};
    newEditing = {};
    newActivity: Activity[];
    selectedRowActivity = [];
    selectedDownwardRowActivity = [];
    selectedRowProcess = [];
    selectedActivity: Activity;
    selectedDownwardActivity: Activity;
    selectedProcess: Process;
    upwardActivity: string;
    downwardActivity: string;
    timePeriod: number;

    constructor(private httpService: HttpService,
        private activityDialogService: ActivityDialogService,
        private processDialogService: ProcessDialogService,
        private viewContainerRef: ViewContainerRef,
        public dialog: MdDialog) {
        this.selectedActivity = null;
    }

    onSelect({ selected }) {
        this.selectedActivity = selected[0];
    }

    onDownwardSelect({ selected }) {
        this.selectedDownwardActivity = selected[0];
    }

    onProcessSelect({ selected }) {
        this.selectedProcess = selected[0];
    }

    executeActivity(activity: Activity, section?: string) {
        this.activityDialogService
            .confirm(activity, this.viewContainerRef, section)
            .subscribe(res => {
                if (res && !activity) {
                    this.activities = null;
                    this.httpService.addActivity({
                        name: res.name,
                        units: +res.units,
                        inputProdRate: res.inputProdRate,
                        duration: +res.duration,
                        aggressiveDuration: +res.aggressiveDuration,
                        safeProductivityRate: +res.safeProductivityRate,
                        aggressiveProductivityRate: +res.aggressiveProductivityRate,
                        startToFinish: +res.startToFinish,
                        unitDelta: +res.unitDelta,
                        durationFunction: res.durationFunction,
                        dependencies: res.activitySelected.join(',').trim(),
                        processId: res.processSelected,
                        section: section
                    }).subscribe(() => {
                        this.ngOnInit();
                    });
                }
                else if (res && activity) {
                    this.activities = null;
                    this.httpService.updateActivity({
                        id: res.id,
                        name: res.name,
                        units: +res.units,
                        inputProdRate: res.inputProdRate,
                        duration: +res.duration,
                        aggressiveDuration: +res.aggressiveDuration,
                        safeProductivityRate: +res.safeProductivityRate,
                        aggressiveProductivityRate: +res.aggressiveProductivityRate,
                        startToFinish: +res.startToFinish,
                        unitDelta: +res.unitDelta,
                        durationFunction: res.durationFunction,
                        dependencies: res.activitySelected.join(',').trim(),
                        processId: res.processSelected,
                        section: res.section
                    }).subscribe(() => {
                        this.ngOnInit();
                    });
                }
            });
    }

    executeProcess(process: Process) {
        this.processDialogService
            .confirm(process, this.viewContainerRef)
            .subscribe(res => {
                if (res && !process) {
                    this.processes = null;
                    this.httpService.addProcess({
                        name: res.name
                    }).subscribe(() => {
                        this.getProcesses();
                        this.getGraph(false);
                    });
                }
                else if (res && process) {
                    this.activities = null;
                    this.httpService.updateProcess({
                        id: res.id,
                        name: res.name
                    }).subscribe(() => {
                        this.getProcesses();
                        this.getGraph(false);
                    });
                }
            });
    }

    deleteActivity(selectedActivity) {
        this.httpService.deleteActivity(selectedActivity.id).
            subscribe(() => this.ngOnInit());
    }

    deleteProcess() {
        this.httpService.deleteProcess(this.selectedProcess.id).
            subscribe(() => this.ngOnInit());
    }

    getActivities() {
        this.httpService.getActivities()
            .subscribe(
            activities => {
                let allActivities = activities.map(a => {
                    a.dependenciesStr = a.dependencies.split(',').map(d => {
                        var match = activities.find(ac => ac.id == +d);
                        if (match) {
                            return match.name;
                        }
                        return null
                    });
                    return a;
                });
                this.activities = allActivities.filter(a => a.section === 'UPWARD');
                this.downwardActivities = allActivities.filter(a => a.section === 'DOWNWARD');
            },
            error => this.errorMessage = <any>error
            );
    }

    getProcesses() {
        this.httpService.getProcesses()
            .subscribe(
            processes => {
                this.processes = processes;
            },
            error => this.errorMessage = <any>error
            );
    }

    getGraph(withCriticalPath) {
        this.httpService.getGraph(withCriticalPath)
            .subscribe(
            graphConfig => this.graphConfig = graphConfig,
            error => this.errorMessage = <any>error
            );
    }

    getLink() {
        this.httpService.getLink()
            .subscribe(link => {
                if (link != null) {
                    this.upwardActivity = link.upwardActivity;
                    this.downwardActivity = link.downwardActivity;
                    this.timePeriod = link.timePeriod;
                }
            })
    }

    updateLink() {
        this.httpService.updateLink({
            upwardActivity: this.upwardActivity,
            downwardActivity: this.downwardActivity,
            timePeriod: this.timePeriod
        }).subscribe();
    }

    ngOnInit() {
        this.getActivities();
        this.getProcesses();
        this.getLink();
        this.getGraph(false);
    }
}

import { ActivityDialogService } from './activityDialog.service';
import { MaterialModule } from '@angular/material';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ActivityDialog }   from './activityDialog.component';
import { HttpService } from './app.httpService';
@NgModule({
    imports: [
        FormsModule,
        BrowserModule,
        MaterialModule.forRoot(),
    ],
    exports: [
        ActivityDialog,
    ],
    declarations: [
        ActivityDialog,
    ],
    providers: [
        ActivityDialogService,
        HttpService
    ],
    entryComponents: [
        ActivityDialog,
    ],
    schemas: [ 
        CUSTOM_ELEMENTS_SCHEMA 
    ],
})
export class ActivityDialogModule { }

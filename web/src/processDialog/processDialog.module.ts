import { ProcessDialogService } from './processDialog.service';
import { MaterialModule } from '@angular/material';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ProcessDialog }   from './processDialog.component';
import { HttpService } from '../app/app.httpService';
@NgModule({
    imports: [
        FormsModule,
        BrowserModule,
        MaterialModule.forRoot(),
    ],
    exports: [
        ProcessDialog,
    ],
    declarations: [
        ProcessDialog,
    ],
    providers: [
        ProcessDialogService,
        HttpService
    ],
    entryComponents: [
        ProcessDialog,
    ],
    schemas: [ 
        CUSTOM_ELEMENTS_SCHEMA 
    ],
})
export class ProcessDialogModule { }

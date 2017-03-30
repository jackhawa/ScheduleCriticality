import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ChartModule } from 'angular2-highcharts';
import { AppComponent } from './app.component';
import { MaterialModule } from '@angular/material';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ActivityDialogModule } from './activityDialog.module';
import { ProcessDialogModule } from '../processDialog/processDialog.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HighchartsFactory } from './highChartsFactory';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    ChartModule.forRoot(HighchartsFactory),
    MaterialModule.forRoot(),
    NgxDatatableModule,
    ActivityDialogModule,
    ProcessDialogModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class AppModule { }

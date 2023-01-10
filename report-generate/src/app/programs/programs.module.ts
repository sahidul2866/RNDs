import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProgramsRoutingModule } from './programs-routing.module';
import { ReportComponent } from './report/report.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    ReportComponent
  ],
  imports: [
    CommonModule,
    ProgramsRoutingModule,
    FormsModule,
    HttpClientModule
  ]
})
export class ProgramsModule { 
}

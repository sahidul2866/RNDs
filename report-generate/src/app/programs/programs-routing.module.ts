import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportComponent } from './report/report.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Document'
    },
    children: [
      {
        path: 'report',
        component: ReportComponent,
        data: {
          title: 'Report',
          reuse:true,
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProgramsRoutingModule { }

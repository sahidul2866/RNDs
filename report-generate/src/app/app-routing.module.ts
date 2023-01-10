import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  {
    path: '',
    data: {
      title: 'Home',
    },
    children: [
      {
        path: 'programs',
        loadChildren: () =>
          import('./programs/programs.module').then((m) => m.ProgramsModule)
      },
    ]
  }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

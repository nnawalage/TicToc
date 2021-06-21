import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoanComparisonComponent } from './loan-comparison/loan-comparison.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/loancomparison',
    pathMatch: 'full'
  },
  {
    path: 'loancomparison',
    component: LoanComparisonComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

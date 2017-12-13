import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";

import { TramsComponent }      from './trams/trams.component';
import { DashboardComponent }   from './dashboard/dashboard.component';
import { TramDetailComponent } from "./tram-detail/tram-detail.component";


const routes: Routes = [
  { path: 'trams', component: TramsComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'detail/:id', component: TramDetailComponent },
];

@NgModule({
  exports: [ RouterModule ],
  imports: [ RouterModule.forRoot(routes) ]
})
export class AppRoutingModule { }

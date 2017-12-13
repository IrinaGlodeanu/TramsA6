import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { TramsComponent } from './trams/trams.component';

import { FormsModule } from '@angular/forms';
import { TramDetailComponent } from './tram-detail/tram-detail.component';

import { TramService } from "./tram.service";
import { MessagesComponent } from './messages/messages.component';
import { MessageService } from './message.service';
import { AppRoutingModule } from './/app-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    TramsComponent,
    TramDetailComponent,
    MessagesComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [TramService, MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }

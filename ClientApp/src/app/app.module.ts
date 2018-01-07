import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AppRoutingModule } from './/app-routing.module';
import { AppComponent } from './app.component';
import { MessagesComponent } from './components/messages/messages.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { AlertComponent } from './components/alert/alert.component';
import { TramsComponent } from './components/trams/trams.component';
import { TramDetailComponent } from './components/tram-detail/tram-detail.component';

import { TramService } from './_services/tram/tram.service';
import { MessageService } from './_services/message/message.service';
import { AlertService} from './_services/alert/alert.service';
import { AuthenticationService } from './_services/authentication/authentication.service';
import { UserService } from './_services/user/user.service';

import { AuthGuard } from './_guards/auth.guard';
import { fakeBackendProvider } from './_helpers/fake-backend';
import { JwtInterceptor } from './_helpers/jwt.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    AlertComponent,
    LoginComponent,
    RegisterComponent,
    TramsComponent,
    TramDetailComponent,
    MessagesComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    AuthGuard,
    TramService,
    MessageService,
    AlertService,
    UserService,
    AuthenticationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },

    // pentru a verifica daca merge autentificare corect
    fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

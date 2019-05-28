import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { BsDropdownModule } from "ngx-bootstrap";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { AuthService } from "./_servicios/auth.service";
import { PrincipalComponent } from "./principal/principal.component";
import { RegistroComponent } from "./registro/registro.component";
import { ErrorInterceptorProvider } from "./_servicios/error-interceptor";
import { AlertifyService } from "./_servicios/alertify.service";
import { CoderosListaComponent } from "./coderos-lista/coderos-lista.component";
import { CoderosFavComponent } from './coderos-fav/coderos-fav.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { AuthGuard } from './_guards/auth.guard';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      PrincipalComponent,
      RegistroComponent,
      CoderosListaComponent,
      CoderosFavComponent,
      MensajesComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot()
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule {}

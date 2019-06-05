import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { BsDropdownModule } from "ngx-bootstrap";
import { JwtModule } from "@auth0/angular-jwt";


import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { AuthService } from "./_servicios/auth.service";
import { PrincipalComponent } from "./principal/principal.component";
import { RegistroComponent } from "./registro/registro.component";
import { ErrorInterceptorProvider } from "./_servicios/error-interceptor";
import { AlertifyService } from "./_servicios/alertify.service";
import { CoderosListaComponent } from "./coderos-lista/coderos-lista.component";
import { CoderosFavComponent } from './favoritos/coderos-fav/coderos-fav.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { AuthGuard } from './_guards/auth.guard';
import { UsuarioService } from './_servicios/usuario.service';
import { TarjetasFavComponent } from './favoritos/tarjetas-fav/tarjetas-fav.component';
import { FavDetallesComponent } from './favoritos/fav-detalles/fav-detalles.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      PrincipalComponent,
      RegistroComponent,
      CoderosListaComponent,
      CoderosFavComponent,
      MensajesComponent,
      TarjetasFavComponent,
      FavDetallesComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      UsuarioService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule {}

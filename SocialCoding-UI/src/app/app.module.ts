import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import {
  BsDropdownModule,
  TabsModule,
  BsDatepickerModule,
  PaginationModule
} from "ngx-bootstrap";
import { JwtModule } from "@auth0/angular-jwt";
import { NgxGalleryModule } from "ngx-gallery";
import { FileUploadModule } from "ng2-file-upload";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { AuthService } from "./_servicios/auth.service";
import { PrincipalComponent } from "./principal/principal.component";
import { RegistroComponent } from "./registro/registro.component";
import { ErrorInterceptorProvider } from "./_servicios/error-interceptor";
import { AlertifyService } from "./_servicios/alertify.service";
import { CoderosListaComponent } from "./coderos-lista/coderos-lista.component";
import { CoderosFavComponent } from "./favoritos/coderos-fav/coderos-fav.component";
import { MensajesComponent } from "./mensajes/mensajes.component";
import { AuthGuard } from "./_guards/auth.guard";
import { UsuarioService } from "./_servicios/usuario.service";
import { TarjetasFavComponent } from "./favoritos/tarjetas-fav/tarjetas-fav.component";
import { FavDetallesComponent } from "./favoritos/fav-detalles/fav-detalles.component";
import { FavDetalleResolver } from "./_resolvers/fav-detalle.resolver";
import { FavListaResolver } from "./_resolvers/fav-lista.resolver";
import { FavEditarComponent } from "./favoritos/fav-editar/fav-editar.component";
import { FavEditarResolver } from "./_resolvers/fav-editar.resolver";
import { NoGuardado } from "./_resolvers/no-guardado.guard";
import { ImagenEditarComponent } from "./favoritos/imagen-editar/imagen-editar.component";

export function tokenGetter() {
  return localStorage.getItem("token");
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
    FavDetallesComponent,
    FavEditarComponent,
    ImagenEditarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    PaginationModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    BsDropdownModule.forRoot(),
    NgxGalleryModule,
    FileUploadModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5000"],
        blacklistedRoutes: ["localhost:5000/api/auth"]
      }
    })
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AlertifyService,
    AuthGuard,
    UsuarioService,
    FavDetalleResolver,
    FavListaResolver,
    FavEditarResolver,
    NoGuardado
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

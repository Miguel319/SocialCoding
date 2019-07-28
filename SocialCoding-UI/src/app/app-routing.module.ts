import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { PrincipalComponent } from "./principal/principal.component";
import { CoderosFavComponent } from "./favoritos/coderos-fav/coderos-fav.component";
import { CoderosListaComponent } from "./coderos-lista/coderos-lista.component";
import { MensajesComponent } from "./mensajes/mensajes.component";
import { AuthGuard } from "./_guards/auth.guard";
import { FavDetallesComponent } from "./favoritos/fav-detalles/fav-detalles.component";
import { FavDetalleResolver } from "./_resolvers/fav-detalle.resolver";
import { FavListaResolver } from "./_resolvers/fav-lista.resolver";
import { FavEditarComponent } from "./favoritos/fav-editar/fav-editar.component";
import { FavEditarResolver } from "./_resolvers/fav-editar.resolver";
import { NoGuardado } from "./_resolvers/no-guardado.guard";
import { MeGustasResolver } from "./_resolvers/meGustas.resolver";
import { MensajesResolver } from "./_resolvers/mensajes.resolver";

const routes: Routes = [
  { path: "", component: PrincipalComponent },
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      {
        path: "coderos",
        component: CoderosFavComponent,
        resolve: { usuarios: FavListaResolver }
      },
      {
        path: "coderos/:id",
        component: FavDetallesComponent,
        resolve: { usuario: FavDetalleResolver }
      },
      {
        path: "fav/editar",
        component: FavEditarComponent,
        resolve: { usuario: FavEditarResolver },
        canDeactivate: [NoGuardado]
      },
      {
        path: "mensajes",
        component: MensajesComponent,
        resolve: { mensajes: MensajesResolver }
      },
      {
        path: "meGustas",
        component: CoderosListaComponent,
        resolve: { usuarios: MeGustasResolver }
      }
    ]
  },
  { path: "**", redirectTo: "", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

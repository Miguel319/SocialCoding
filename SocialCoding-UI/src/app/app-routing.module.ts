import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { PrincipalComponent } from "./principal/principal.component";
import { CoderosFavComponent } from "./coderos-fav/coderos-fav.component";
import { CoderosListaComponent } from "./coderos-lista/coderos-lista.component";
import { MensajesComponent } from "./mensajes/mensajes.component";
import { AuthGuard } from "./_guards/auth.guard";

const routes: Routes = [
  { path: "", component: PrincipalComponent },
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      { path: "coderos", component: CoderosListaComponent },
      { path: "mensajes", component: MensajesComponent },
      { path: "favoritos", component: CoderosFavComponent }
    ]
  },
  { path: "**", redirectTo: "", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

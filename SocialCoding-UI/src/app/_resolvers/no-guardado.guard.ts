import { Injectable } from "@angular/core";
import { CanDeactivate } from "@angular/router";
import { FavEditarComponent } from "../favoritos/fav-editar/fav-editar.component";

@Injectable()
export class NoGuardado implements CanDeactivate<FavEditarComponent> {
  canDeactivate(component: FavEditarComponent) {
    if (component.editarFormulario.dirty) {
      return confirm("¿Seguro que quieres salir sin guardar los cambios?");
    }
    return true;
  }
}

import { Injectable } from "@angular/core";
import { Usuario } from "../_modelos/usuario";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { UsuarioService } from "../_servicios/usuario.service";
import { AlertifyService } from "../_servicios/alertify.service";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class FavListaResolver implements Resolve<Usuario[]> {
  constructor(
    private usuarioServicio: UsuarioService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Usuario[]> {
    return this.usuarioServicio.getUsuarios().pipe(
      catchError(err => {
        this.alertify.error("Problema al obtener los datos");
        this.router.navigate(["/principal"]);
        return of(null);
      })
    );
  }
}

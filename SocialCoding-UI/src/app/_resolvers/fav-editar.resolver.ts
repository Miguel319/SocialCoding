import { Injectable } from "@angular/core";
import { Usuario } from "../_modelos/usuario";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { UsuarioService } from "../_servicios/usuario.service";
import { AlertifyService } from "../_servicios/alertify.service";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { AuthService } from "../_servicios/auth.service";

@Injectable()
export class FavEditarResolver implements Resolve<Usuario> {
  constructor(
    private usuarioServicio: UsuarioService,
    private router: Router,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Usuario> {
    return this.usuarioServicio.getUsuario(this.authService.tokenD.nameid).pipe(
      catchError(err => {
        console.error(err);
        this.alertify.error("Problema al obtener los datos.");
        this.router.navigate(["/favoritos"]);
        return of(null);
      })
    );
  }
}

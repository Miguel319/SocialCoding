import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { UsuarioService } from "../_servicios/usuario.service";
import { AlertifyService } from "../_servicios/alertify.service";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Mensaje } from '../_modelos/mensaje';
import { AuthService } from '../_servicios/auth.service';

@Injectable()
export class MensajesResolver implements Resolve<Mensaje[]> {
  noPagina = 1;
  tamanoPagina = 12;
  contenedorMensaje = 'noLeido';

  constructor(
    private usuarioServicio: UsuarioService,
    private router: Router,
    private alertify: AlertifyService,
    private authServicio: AuthService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Mensaje[]> {
    return this.usuarioServicio
      .obtenerMensajes(this.authServicio.tokenD.nameid, 
        this.noPagina, this.tamanoPagina, this.contenedorMensaje)
      .pipe(
        catchError(err => {
          this.alertify.error("Problema al obtener los mensajes");
          this.router.navigate(["/principal"]);
          return of(null);
        })
      );
  }
}

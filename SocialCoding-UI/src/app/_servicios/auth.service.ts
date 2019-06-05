import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { UsuarioAuth } from "../_modelos/usuario-auth";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: "root"
})
export class AuthService {
  urlB = environment.apiUrl+ "auth/";;
  jwtHelper = new JwtHelperService();
  tokenD: any;

  constructor(private http: HttpClient) {}

  iniciarSesion(usuario: UsuarioAuth) {
    return this.http.post(this.urlB + "isesion", usuario).pipe(
      map((res: any) => {
        const usuari = res;
        if (usuari) {
          localStorage.setItem("token", usuari.token);
          this.tokenD = this.jwtHelper.decodeToken(usuari.token);
          console.log(this.tokenD);
        }
      })
    );
  }

  registrar(usuario: UsuarioAuth) {
    return this.http.post(this.urlB + "registrar", usuario);
  }

  limpiarCampos(usuario: UsuarioAuth) {
    usuario.nombreUsuario = "";
    usuario.contra = "";
  }

  sesionIniciada() {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }
}

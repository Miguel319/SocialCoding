import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { UsuarioAuth } from "../_modelos/usuario-auth";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from "src/environments/environment";
import { Usuario } from "../_modelos/usuario";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  urlB = environment.apiUrl + "auth/";
  jwtHelper = new JwtHelperService();
  tokenD: any;
  usuarioActual: Usuario;

  constructor(private http: HttpClient) {}

  iniciarSesion(usuario: UsuarioAuth) {
    return this.http.post(this.urlB + "isesion", usuario).pipe(
      map((res: any) => {
        const usuario = res;
        if (usuario) {
          localStorage.setItem("token", usuario.token);
          localStorage.setItem("usuario", JSON.stringify(usuario.usuario));
          this.tokenD = this.jwtHelper.decodeToken(usuario.token);
          this.usuarioActual = usuario.usuario;
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

  getTokenD() {
    return this.tokenD;
  }

  sesionIniciada() {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }
}

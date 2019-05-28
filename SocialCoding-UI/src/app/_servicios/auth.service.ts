import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Usuario } from "../_modelos/usuario.model";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  urlB = "http://localhost:5000/api/auth/";
  jwtHelper = new JwtHelperService();
  tokenD: any;

  constructor(private http: HttpClient) {}

  iniciarSesion(usuario: Usuario) {
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

  registrar(usuario: Usuario) {
    return this.http.post(this.urlB + "registrar", usuario);
  }

  limpiarCampos(usuario: Usuario) {
    usuario.nombreUsuario = "";
    usuario.contra = "";
  }

  sesionIniciada() {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }
}

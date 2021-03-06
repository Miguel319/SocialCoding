import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { UsuarioAuth } from "../_modelos/usuario-auth";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from "src/environments/environment";
import { Usuario } from "../_modelos/usuario";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  urlB = environment.apiUrl + "auth/";
  jwtHelper = new JwtHelperService();
  tokenD: any;
  usuarioActual: Usuario;
  imagenUrl = new BehaviorSubject<string>("../../assets/usuario.png");
  imagenUrlActual = this.imagenUrl.asObservable();

  constructor(private http: HttpClient) {}

  cambiarImagen(imagenUrl: string) {
    this.imagenUrl.next(imagenUrl);
  }

  iniciarSesion(usuario: any) {
    return this.http.post(this.urlB + "isesion", usuario).pipe(
      map((res: any) => {
        const usuario = res;
        if (usuario) {
          localStorage.setItem("token", usuario.token);
          localStorage.setItem("usuario", JSON.stringify(usuario.usuario));
          this.tokenD = this.jwtHelper.decodeToken(usuario.token);
          this.usuarioActual = usuario.usuario;
          this.cambiarImagen(this.usuarioActual.imagenUrl);
        }
      })
    );
  }

  registrar(usuario: Usuario) {
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

import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Usuario } from '../_modelos/usuario.model';

@Injectable({
  providedIn: "root"
})
export class AuthService {
  urlB = "http://localhost:5000/api/auth/";

  constructor(private http: HttpClient) {}

  iniciarSesion(usuario: Usuario) {
    return this.http.post(this.urlB + "isesion", usuario).pipe(
      map((res: any) => {
        const usuari = res;
        if (usuari) {
          localStorage.setItem("token", usuari.token);
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
}

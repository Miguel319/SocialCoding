import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Usuario } from "../_modelos/usuario";

@Injectable({
  providedIn: "root"
})
export class UsuarioService {
  urlBase = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsuarios(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(this.urlBase + "usuarios");
  }

  getUsuario(id: number | string): Observable<Usuario> {
    return this.http.get<Usuario>(this.urlBase + "usuarios/" + id);
  }

  actualizarUsuario(id: number, usuario: Usuario) {
    return this.http.put(this.urlBase + 'usuarios/' + id, usuario);
  }

  establecerFotoDePerfil(usuarioId: number, id: number) {
    return this.http.post(this.urlBase + "usuarios/" + usuarioId + "/imagenes/" + id + "/establecerDePerfil", {});
  }
  
}

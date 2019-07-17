import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Usuario } from "../_modelos/usuario";
import { ResultadoPaginado } from "../_modelos/paginacion";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class UsuarioService {
  urlBase = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsuarios(
    pagina?,
    elementosPorPagina?,
    meGustasParam?
  ): Observable<ResultadoPaginado<Usuario[]>> {
    const resultadoPaginado: ResultadoPaginado<
      Usuario[]
    > = new ResultadoPaginado<Usuario[]>();

    let params = new HttpParams();

    if (pagina != null && elementosPorPagina != null) {
      params = params.append("noPagina", pagina);
      params = params.append("tamanoPagina", elementosPorPagina);
    }

    if (meGustasParam === "MeGustadores") {
      params = params.append("meGustadores", "true");
    }

    if (meGustasParam === "MeGustas") {
      params = params.append("meGustas", "true");
    }

    return this.http
      .get<Usuario[]>(this.urlBase + "usuarios", {
        observe: "response",
        params
      })
      .pipe(
        map(response => {
          resultadoPaginado.resultado = response.body;
          if (response.headers.get("Paginacion") != null) {
            resultadoPaginado.paginacion = JSON.parse(
              response.headers.get("Paginacion")
            );
          }
          return resultadoPaginado;
        })
      );
  }

  getUsuario(id: number | string): Observable<Usuario> {
    return this.http.get<Usuario>(this.urlBase + "usuarios/" + id);
  }

  actualizarUsuario(id: number, usuario: Usuario) {
    return this.http.put(this.urlBase + "usuarios/" + id, usuario);
  }

  establecerFotoDePerfil(usuarioId: number, id: number) {
    return this.http.post(
      this.urlBase +
        "usuarios/" +
        usuarioId +
        "/imagenes/" +
        id +
        "/establecerDePerfil",
      {}
    );
  }

  eliminarFoto(usuarioId: number, id: number) {
    return this.http.delete(
      this.urlBase + "usuarios/" + usuarioId + "/imagenes/" + id
    );
  }

  enviarMeGusta(id: number, recibidorId: number) {
    return this.http.post(
      this.urlBase + "usuarios/" + id + "/meGusta/" + recibidorId,
      {}
    );
  }
}

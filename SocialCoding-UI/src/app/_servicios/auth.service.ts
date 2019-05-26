import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  urlB = "http://localhost:5000/api/auth/";

  constructor(private http: HttpClient) {}

  iniciarSesion(usuario: any) {
    return this.http.post(this.urlB + "isesion", usuario).pipe(
      map((res: any) => {
        const usuari = res;
        if (usuari) {
          localStorage.setItem("token", usuari.token);
        }
      })
    );
  }

  registrar(usuario: any) {
    return this.http.post("http://localhost:5000/api/auth/registrar", usuario);
  }
}

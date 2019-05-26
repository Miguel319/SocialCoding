import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: "app-principal",
  templateUrl: "./principal.component.html",
  styleUrls: ["./principal.component.css"]
})
export class PrincipalComponent implements OnInit {
  modoRegistro = false;
  valores : any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getValores();
  }

  registrarCambio() {
    this.modoRegistro = true;
  }

  getValores() {
    this.http.get("http://localhost:5000/api/values").subscribe(
      res => {
        this.valores = res;
        console.log(this.valores);
      },
      err => console.error(err)
    );
  }

  cancelarModoRegistro(modoRegistro: boolean) {
    this.modoRegistro = modoRegistro;
  }
}

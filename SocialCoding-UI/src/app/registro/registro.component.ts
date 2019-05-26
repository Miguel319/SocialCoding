import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { AuthService } from "../_servicios/auth.service";

@Component({
  selector: "app-registro",
  templateUrl: "./registro.component.html",
  styleUrls: ["./registro.component.css"]
})
export class RegistroComponent implements OnInit {
  @Output() registroCancelado = new EventEmitter();
  usuario: any = {};

  constructor(private authServicio: AuthService) {}

  ngOnInit() {}

  registrar() {
    this.authServicio
      .registrar(this.usuario)
      .subscribe(
        res => console.log("Registro exitoso"),
        err => console.error(err)
      );
  }

  cancelar() {
    this.registroCancelado.emit(false);
    console.log("Cancelado!");
  }
}

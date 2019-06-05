import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { AuthService } from "../_servicios/auth.service";
import { UsuarioAuth } from "../_modelos/usuario-auth";
import { AlertifyService } from "../_servicios/alertify.service";

@Component({
  selector: "app-registro",
  templateUrl: "./registro.component.html",
  styleUrls: ["./registro.component.css"]
})
export class RegistroComponent implements OnInit {
  @Output() registroCancelado = new EventEmitter();
  usuario: UsuarioAuth;

  constructor(
    private authServicio: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.usuario = {
      nombreUsuario: "",
      contra: ""
    };
  }

  registrar() {
    this.authServicio.registrar(this.usuario).subscribe(
      res => {
        this.alertify.exito("¡Usuario registrado satisfactoriamente!");
        this.authServicio.limpiarCampos(this.usuario);
      },
      err => {
        this.alertify.error(err);
        console.error(err);
      }
    );
  }

  cancelar() {
    this.registroCancelado.emit(false);
  }
}

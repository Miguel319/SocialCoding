import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { AuthService } from "../_servicios/auth.service";
import { Usuario } from '../_modelos/usuario.model';

@Component({
  selector: "app-registro",
  templateUrl: "./registro.component.html",
  styleUrls: ["./registro.component.css"]
})
export class RegistroComponent implements OnInit {
  @Output() registroCancelado = new EventEmitter();
  usuario: Usuario;

  constructor(private authServicio: AuthService) {}

  ngOnInit() {
    this.usuario = {
      nombreUsuario: "",
      contra: ""
    };
  }

  registrar() {
    this.authServicio
      .registrar(this.usuario)
      .subscribe(
        res => {
          console.log("Registro exitoso");
          this.authServicio.limpiarCampos(this.usuario)
        },
        err => console.error(err)
      );
  }

  cancelar() {
    this.registroCancelado.emit(false);
    console.log("Cancelado!");
  }
}

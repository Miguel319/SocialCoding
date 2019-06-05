import { Component, OnInit } from "@angular/core";
import { Usuario } from "../../_modelos/usuario";
import { AlertifyService } from "../../_servicios/alertify.service";
import { UsuarioService } from "../../_servicios/usuario.service";

@Component({
  selector: "app-coderos-fav",
  templateUrl: "./coderos-fav.component.html",
  styleUrls: ["./coderos-fav.component.css"]
})
export class CoderosFavComponent implements OnInit {
  usuarios: Usuario[];

  constructor(
    private usuarioServicio: UsuarioService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.cargarUsuarios();
  }

  cargarUsuarios() {
    this.usuarioServicio
      .getUsuarios()
      .subscribe(
        (usuarios: Usuario[]) => (this.usuarios = usuarios),
        err => this.alertify.error(err)
      );
  }
}

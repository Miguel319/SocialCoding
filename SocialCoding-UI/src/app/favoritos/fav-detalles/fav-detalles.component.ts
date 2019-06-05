import { Component, OnInit } from "@angular/core";
import { Usuario } from "src/app/_modelos/usuario";
import { UsuarioService } from "src/app/_servicios/usuario.service";
import { AlertifyService } from "src/app/_servicios/alertify.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-fav-detalles",
  templateUrl: "./fav-detalles.component.html",
  styleUrls: ["./fav-detalles.component.css"]
})
export class FavDetallesComponent implements OnInit {
  usuario: Usuario;

  constructor(
    private usuarioService: UsuarioService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.cargarUsuario();
  }

  cargarUsuario() {
    this.usuarioService
      .getUsuario(+this.route.snapshot.params["id"])
      .subscribe(
        (usuario: Usuario) => (this.usuario = usuario),
        err => this.alertify.error(err)
      );
  }
}

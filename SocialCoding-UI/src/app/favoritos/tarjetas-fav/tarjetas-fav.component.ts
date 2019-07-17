import { Component, OnInit, Input } from "@angular/core";
import { Usuario } from "src/app/_modelos/usuario";
import { AuthService } from "src/app/_servicios/auth.service";
import { AlertifyService } from "src/app/_servicios/alertify.service";
import { UsuarioService } from "src/app/_servicios/usuario.service";

@Component({
  selector: "app-tarjetas-fav",
  templateUrl: "./tarjetas-fav.component.html",
  styleUrls: ["./tarjetas-fav.component.css"]
})
export class TarjetasFavComponent implements OnInit {
  @Input() usuario: Usuario;

  constructor(
    private authServicio: AuthService,
    private usuarioServicio: UsuarioService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}

  enviarMeGusta(id: number) {
    this.usuarioServicio
      .enviarMeGusta(this.authServicio.tokenD.nameid, id)
      .subscribe(
        res =>
          this.alertify.exito(`Le has dado me gusta a ${this.usuario.alias}`),
        err => this.alertify.error(err)
      );

    /*this.usuarioServicio.enviarMeGusta(this.authServicio.tokenD.nameid, id)
      .subscribe(
        data =>  this.alertify.exito('Le has dado Me Gusta a '+this.usuario.alias),
        error => this.alertify.error(error)
      );*/
  }
}

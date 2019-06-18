import { Component, OnInit, ViewChild, HostListener } from "@angular/core";
import { Usuario } from "src/app/_modelos/usuario";
import { ActivatedRoute } from "@angular/router";
import { AlertifyService } from "src/app/_servicios/alertify.service";
import { NgForm } from "@angular/forms";
import { UsuarioService } from "src/app/_servicios/usuario.service";
import { AuthService } from "src/app/_servicios/auth.service";

@Component({
  selector: "app-fav-editar",
  templateUrl: "./fav-editar.component.html",
  styleUrls: ["./fav-editar.component.css"]
})
export class FavEditarComponent implements OnInit {
  usuario: Usuario;
  @ViewChild("formularioEdicion") editarFormulario: NgForm;
  @HostListener("window:beforeunload", ["$event"])
  unloadNotification($event: any) {
    if (this.editarFormulario.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private usuarioServicio: UsuarioService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.usuario = data["usuario"];
    });
  }

  actualizarUsuario() {
    this.usuarioServicio
      .actualizarUsuario(this.authService.tokenD.nameid, this.usuario)
      .subscribe(
        data => {
          this.alertify.exito("¡Perfil actualizado satisfactoriamente!");
          this.editarFormulario.reset(this.usuario);
        },
        err => this.alertify.error(err)
      );
  }
}

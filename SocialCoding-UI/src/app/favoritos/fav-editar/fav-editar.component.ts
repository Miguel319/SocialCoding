import { Component, OnInit, ViewChild } from "@angular/core";
import { Usuario } from "src/app/_modelos/usuario";
import { ActivatedRoute } from "@angular/router";
import { AlertifyService } from "src/app/_servicios/alertify.service";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-fav-editar",
  templateUrl: "./fav-editar.component.html",
  styleUrls: ["./fav-editar.component.css"]
})
export class FavEditarComponent implements OnInit {
  @ViewChild("formularioEdicion") editarFormulario: NgForm;
  usuario: Usuario;

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.usuario = data["usuario"];
    });
  }

  actualizarUsuario() {
    console.log(this.usuario);
    this.alertify.exito("¡Perfil actualizado satisfactoriamente!");
    this.editarFormulario.reset(this.usuario);
  }
}

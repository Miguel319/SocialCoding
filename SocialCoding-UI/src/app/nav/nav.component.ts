import { Component, OnInit } from "@angular/core";
import { AuthService } from "../_servicios/auth.service";
import { Usuario } from "../_modelos/usuario.model";
import { AlertifyService } from "../_servicios/alertify.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  usuario: Usuario;

  constructor(
    private authServicio: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {
    this.usuario = {
      nombreUsuario: "",
      contra: ""
    };
  }

  getAuthServicio() {
    return this.authServicio;
  }

  iniciarSesion() {
    this.authServicio.iniciarSesion(this.usuario).subscribe(
      res => {
        this.alertify.exito("¡Sesión iniciada exitosamente!");
        this.authServicio.limpiarCampos(this.usuario);
      },
      err => {
        this.alertify.error("¡Error al iniciar sesión!");
        console.log(err);
      },
      () => {
        this.router.navigate(["/miembros"]);
      }
    );
  }

  sesionIniciada() {
    return this.authServicio.sesionIniciada();
  }

  cerrarSesion() {
    localStorage.removeItem("token");
    this.alertify.advertencia("!Sesión cerrada exitosamente!");
    this.router.navigate(["/principal"]);
  }
}

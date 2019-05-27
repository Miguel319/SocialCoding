import { Component, OnInit } from "@angular/core";
import { AuthService } from "../_servicios/auth.service";
import { Usuario } from "../_modelos/usuario.model";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  usuario: Usuario;

  constructor(private authServicio: AuthService) {}

  ngOnInit() {
    this.usuario = {
      nombreUsuario: "",
      contra: ""
    };
  }

  iniciarSesion() {
    this.authServicio.iniciarSesion(this.usuario).subscribe(
      res => {
        console.log("Sesión iniciada exitosamente");
        this.authServicio.limpiarCampos(this.usuario);
      },
      err => {
        console.log(err);
      }
    );
  }

  sesionIniciada() {
    const token = localStorage.getItem("token");
    return !!token;
  }

  cerrarSesion() {
    localStorage.removeItem("token");
    console.log("Sesión cerrada");
  }
}

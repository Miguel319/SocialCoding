import { Component, OnInit } from "@angular/core";
import { Usuario } from "../../_modelos/usuario";
import { AlertifyService } from "../../_servicios/alertify.service";
import { UsuarioService } from "../../_servicios/usuario.service";
import { ActivatedRoute } from "@angular/router";
import { Paginacion, ResultadoPaginado } from "src/app/_modelos/paginacion";

@Component({
  selector: "app-coderos-fav",
  templateUrl: "./coderos-fav.component.html",
  styleUrls: ["./coderos-fav.component.css"]
})
export class CoderosFavComponent implements OnInit {
  usuarios: any[];
  paginacion: Paginacion;

  constructor(
    private usuarioServicio: UsuarioService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(
      data => {
        this.usuarios = data["usuarios"].resultado;
        this.paginacion = data["usuarios"].paginacion;
      },
      err => this.alertify.error(err)
    );
  }

  paginaCambiada(event: any): void {
    this.paginacion.paginaActual = event.page;
    console.log(this.paginacion.paginaActual);
    this.cargarUsuarios();
  }

  cargarUsuarios() {
    this.usuarioServicio
      .getUsuarios(
        this.paginacion.paginaActual,
        this.paginacion.elementosPorPagina
      )
      .subscribe(
        (res: ResultadoPaginado<Usuario[]>) => {
          this.usuarios = res.resultado;
          this.paginacion = res.paginacion;
        },
        err => this.alertify.error(err)
      );
  }
}

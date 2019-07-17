import { Component, OnInit } from "@angular/core";
import { Usuario } from "../_modelos/usuario";
import { UsuarioService } from "../_servicios/usuario.service";
import { AlertifyService } from "../_servicios/alertify.service";
import { Paginacion, ResultadoPaginado } from "../_modelos/paginacion";
import { AuthService } from "../_servicios/auth.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-coderos-lista",
  templateUrl: "./coderos-lista.component.html",
  styleUrls: ["./coderos-lista.component.css"]
})
export class CoderosListaComponent implements OnInit {
  usuarios: Usuario[];
  paginacion: Paginacion;
  meGustaParam: string;

  constructor(
    private authServicio: AuthService,
    private usuarioServicio: UsuarioService,
    private route: ActivatedRoute,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.usuarios = data["usuarios"].resultado;
      this.paginacion = data["usuarios"].paginacion;
    });
    this.meGustaParam = "MeGustadores";
  }

  paginaCambiada(event: any): void {
    this.paginacion.paginaActual = event.page;
    console.log(this.paginacion.paginaActual);
    this.cargarUsuarios(this.meGustaParam);
  }

  cargarUsuarios(param) {
    this.meGustaParam = param;

    this.usuarioServicio
      .getUsuarios(
        this.paginacion.paginaActual,
        this.paginacion.elementosPorPagina,
        this.meGustaParam
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

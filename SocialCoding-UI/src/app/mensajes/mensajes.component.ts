import { Component, OnInit } from '@angular/core';
import { Mensaje } from '../_modelos/mensaje';
import { Paginacion, ResultadoPaginado } from '../_modelos/paginacion';
import { UsuarioService } from '../_servicios/usuario.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_servicios/alertify.service';
import { AuthService } from '../_servicios/auth.service';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.css']
})
export class MensajesComponent implements OnInit {
  mensajes: Mensaje[];
  paginacion: Paginacion;
  contenedorMensaje = 'noLeido';

  constructor(private usuarioServicio: UsuarioService,
        private authServicio: AuthService,
        private route: ActivatedRoute,
        private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.mensajes = data['mensajes'].result;
      this.paginacion = data['mensajes'].paginacion;
    })
  }

  cargarMensajes() {
    this.usuarioServicio.obtenerMensajes(this.authServicio.tokenD.nameid,
      this.paginacion.paginaActual, this.paginacion.elementosPorPagina,
       this.contenedorMensaje)
       .subscribe((res: ResultadoPaginado<Mensaje[]>) => {
         this.mensajes = res.resultado;
         this.paginacion = res.paginacion;
       }, error => {
         this.alertify.error(error)
       })
  }

  paginadaCambiada(event: any) {
    this.paginacion.paginaActual = event.page;
    this.cargarMensajes();
  }
}

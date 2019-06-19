import { Component, OnInit } from "@angular/core";
import { Usuario } from "src/app/_modelos/usuario";
import { UsuarioService } from "src/app/_servicios/usuario.service";
import { AlertifyService } from "src/app/_servicios/alertify.service";
import { ActivatedRoute } from "@angular/router";
import {
  NgxGalleryOptions,
  NgxGalleryImage,
  NgxGalleryAnimation
} from "ngx-gallery";

@Component({
  selector: "app-fav-detalles",
  templateUrl: "./fav-detalles.component.html",
  styleUrls: ["./fav-detalles.component.css"]
})
export class FavDetallesComponent implements OnInit {
  usuario: Usuario;
  galeriaOpciones: NgxGalleryOptions[];
  galeriaImgs: NgxGalleryImage[];

  constructor(
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(
      data => (this.usuario = data["usuario"]),
      err => this.alertify.error(err)
    );

    this.galeriaOpciones = [
      {
        width: "500px",
        height: "500px",
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];

    this.galeriaImgs = this.getImagenes();
  }

  getImagenes() {
    const imgUrls = [];

    for (let i = 0; i < this.usuario.imagenes.length; i++) {
      imgUrls.push({
        small: this.usuario.imagenes[i].url,
        medium: this.usuario.imagenes[i].url,
        big: this.usuario.imagenes[i].url,
        description: this.usuario.imagenes[i].descripcion
      });
    }
    return imgUrls;
  }
}

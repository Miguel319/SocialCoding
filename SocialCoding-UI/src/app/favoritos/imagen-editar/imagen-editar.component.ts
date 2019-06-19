import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Imagen } from "src/app/_modelos/Imagen";
import { FileUploader } from "ng2-file-upload";
import { environment } from "src/environments/environment";
import { AuthService } from "src/app/_servicios/auth.service";
import { UsuarioService } from "src/app/_servicios/usuario.service";
import { AlertifyService } from "src/app/_servicios/alertify.service";

@Component({
  selector: "app-imagen-editar",
  templateUrl: "./imagen-editar.component.html",
  styleUrls: ["./imagen-editar.component.css"]
})
export class ImagenEditarComponent implements OnInit {
  @Input() imagenes: Imagen[];
  @Output() fotoDePerfil = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  urlBase = environment.apiUrl;
  dePerfilActual: Imagen;

  constructor(
    private authService: AuthService,
    private usuarioServicio: UsuarioService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.inicializarUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  inicializarUploader() {
    this.uploader = new FileUploader({
      url:
        this.urlBase +
        "usuarios/" +
        this.authService.tokenD.nameid +
        "/imagenes",
      authToken: "Bearer " + localStorage.getItem("token"),
      isHTML5: true,
      allowedFileType: ["image"],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = file => (file.withCredentials = false);

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Imagen = JSON.parse(response);
        const imagen = {
          id: res.id,
          url: res.url,
          agregadaEn: res.agregadaEn,
          descripcion: res.descripcion,
          dePerfil: res.dePerfil
        };
        this.imagenes.push(imagen);
      }
    };
  }

  establecerFotoDePerfil(imagen: Imagen) {
    this.usuarioServicio
      .establecerFotoDePerfil(this.authService.tokenD.nameid, imagen.id)
      .subscribe(
        res =>{
          this.dePerfilActual = this.imagenes.filter(p => p.dePerfil === true)[0];
          this.dePerfilActual.dePerfil = false;
          imagen.dePerfil = true;
          this.fotoDePerfil.emit(imagen.url);
          this.alertify.exito("Foto de perfil actualizada satisfactoriamente.");
        },
        err => this.alertify.error("Error al actualizar foto de perfil.")
      );
  }
}

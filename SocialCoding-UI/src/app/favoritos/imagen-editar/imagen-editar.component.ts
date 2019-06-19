import { Component, OnInit, Input } from "@angular/core";
import { Imagen } from "src/app/_modelos/Imagen";
import { FileUploader } from "ng2-file-upload";
import { environment } from "src/environments/environment";
import { AuthService } from "src/app/_servicios/auth.service";

@Component({
  selector: "app-imagen-editar",
  templateUrl: "./imagen-editar.component.html",
  styleUrls: ["./imagen-editar.component.css"]
})
export class ImagenEditarComponent implements OnInit {
  @Input() imagenes: Imagen[];
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  urlBase = environment.apiUrl;

  constructor(private authService: AuthService) {}

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
}

import { Injectable } from "@angular/core";
declare let alertify: any;

@Injectable({
  providedIn: "root"
})
export class AlertifyService {
  constructor() {}

  confirmar(mensaje: string, okCallback: () => any) {
    alertify.confirm(mensaje, (e) => {
      if (e) {
        okCallback();
      } else {}
    });
  }

  exito(mensaje: string) {
    alertify.success(mensaje);
  }

  error(mensaje: string) {
    alertify.error(mensaje);
  }

  advertencia(mensaje: string) {
    alertify.warning(mensaje);
  }

  mensaje(mensaje: string) {
    alertify.message(mensaje);
  }
}

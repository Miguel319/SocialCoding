import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { AuthService } from "../_servicios/auth.service";
import { AlertifyService } from "../_servicios/alertify.service";
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from "@angular/forms";

@Component({
  selector: "app-registro",
  templateUrl: "./registro.component.html",
  styleUrls: ["./registro.component.css"]
})
export class RegistroComponent implements OnInit {
  @Output() registroCancelado = new EventEmitter();
  registroFormulario: FormGroup;

  constructor(
    private authServicio: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.crearFormularioRegistro();
  }

  crearFormularioRegistro() {
    this.registroFormulario = this.fb.group(
      {
        genero: ["masculino"],
        nombreUsuario: ["", Validators.required],
        alias: ["", Validators.required],
        fechaNacimiento: [null, Validators.required],
        ciudad: ["", Validators.required],
        pais: ["", Validators.required],
        contra: ["", [Validators.required, Validators.minLength(4)]],
        confirmarContra: ["", [Validators.required, Validators.minLength(4)]]
      },
      { validator: this.validarContrasenia }
    );
  }

  validarContrasenia(formulario: FormGroup) {
    return formulario.get("contra").value ===
      formulario.get("confirmarContra").value
      ? null
      : { mismatch: true };
  }

  registrar() {
    // this.authServicio.registrar(this.usuario).subscribe(
    //   res => {
    //     this.alertify.exito("¡Usuario registrado satisfactoriamente!");
    //     this.authServicio.limpiarCampos(this.usuario);
    //   },
    //   err => {
    //     this.alertify.error(err);
    //     console.error(err);
    //   }
    // );
    console.log(this.registroFormulario.value);
  }

  cancelar() {
    this.registroCancelado.emit(false);
  }
}

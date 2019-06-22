import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { AuthService } from "../_servicios/auth.service";
import { AlertifyService } from "../_servicios/alertify.service";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { BsDatepickerConfig } from "ngx-bootstrap";
import { Usuario } from "../_modelos/usuario";
import { Router } from "@angular/router";

@Component({
  selector: "app-registro",
  templateUrl: "./registro.component.html",
  styleUrls: ["./registro.component.css"]
})
export class RegistroComponent implements OnInit {
  @Output() registroCancelado = new EventEmitter();
  registroFormulario: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;
  usuario: Usuario;

  constructor(
    private authServicio: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.bsConfig = {
      containerClass: "theme-dark-blue"
    };
    this.crearFormularioRegistro();
  }

  crearFormularioRegistro() {
    this.registroFormulario = this.fb.group(
      {
        genero: ["masculino"],
        nombreUsuario: [
          "",
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(20)
          ]
        ],
        alias: ["", Validators.required],
        fechaNacimiento: [null, Validators.required],
        ciudad: ["", Validators.required],
        pais: ["", Validators.required],
        contra: [
          "",
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(25)
          ]
        ],
        confirmarContra: [
          "",
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(25)
          ]
        ]
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
    if (this.registroFormulario.valid) {
      this.usuario = Object.assign({}, this.registroFormulario.value);
      this.authServicio.registrar(this.usuario).subscribe(
        res => this.alertify.exito("Usuario creado exitosamente"),
        err => this.alertify.error("Error al registrar usuario"),
        () =>
          this.authServicio.iniciarSesion(this.usuario).subscribe(() => {
            this.router.navigate(["/favoritos"]);
          })
      );
    }
    console.log(this.registroFormulario.value);
  }

  cancelar() {
    this.registroCancelado.emit(false);
  }
}

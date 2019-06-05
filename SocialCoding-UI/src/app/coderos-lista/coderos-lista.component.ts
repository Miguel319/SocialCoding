import { Component, OnInit } from '@angular/core';
import { Usuario } from '../_modelos/usuario';
import { UsuarioService } from '../_servicios/usuario.service';
import { AlertifyService } from '../_servicios/alertify.service';

@Component({
  selector: 'app-coderos-lista',
  templateUrl: './coderos-lista.component.html',
  styleUrls: ['./coderos-lista.component.css']
})
export class CoderosListaComponent implements OnInit {
  constructor() {}

  ngOnInit() {
    
  }
}
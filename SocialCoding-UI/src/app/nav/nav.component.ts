import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_servicios/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  usuario: any = {};

  constructor(private authServicio: AuthService) { }

  ngOnInit() {
  }

  iniciarSesion() {
    this.authServicio.iniciarSesion(this.usuario)
      .subscribe(
        res => {
          console.log('Sesión iniciada exitosamente')
        }
      ),
        err => {
          console.error(err)
        }
  }

  sesionIniciada() {
    const token = localStorage.getItem('token');
    return !! token;
  }

  cerrarSesion() {
    localStorage.removeItem('token');
    console.log('Sesión cerrada');
  }
}

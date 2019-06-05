import { Component, OnInit, Input } from '@angular/core';
import { Usuario } from 'src/app/_modelos/usuario';

@Component({
  selector: 'app-tarjetas-fav',
  templateUrl: './tarjetas-fav.component.html',
  styleUrls: ['./tarjetas-fav.component.css']
})
export class TarjetasFavComponent implements OnInit {
  @Input() usuario: Usuario;

  constructor() { }

  ngOnInit() {
  }

}

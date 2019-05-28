import { Component, OnInit } from '@angular/core';
import { AuthService } from './_servicios/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  constructor(private authServicio: AuthService) {

  }

  ngOnInit() {
    const token = localStorage.getItem('token');
    
    if (token) {
      this.authServicio.tokenD = this.jwtHelper.decodeToken(token);
    }
  }
}

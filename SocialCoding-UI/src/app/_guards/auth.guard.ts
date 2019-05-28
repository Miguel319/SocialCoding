import { Injectable } from "@angular/core";
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router
} from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../_servicios/auth.service";
import { AlertifyService } from "../_servicios/alertify.service";

@Injectable({
  providedIn: "root"
})
export class AuthGuard implements CanActivate {
  constructor(
    private authServicio: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  canActivate(): boolean {
    if (this.authServicio.sesionIniciada()) {
      return true;
    }

    this.alertify.error("¡Acceso denegado!");
    this.router.navigate(["/principal"]);
    return false;
  }
}

import { Injectable } from "@angular/core";
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
  HTTP_INTERCEPTORS
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) return throwError(err.statusText);

          const appError = err.headers.get("Application-Error");
          if (appError) {
            console.error(appError);
            return throwError(appError);
          }
          const errorServidor = err.error;
          let modalStateErrors = "";

          if (errorServidor && typeof errorServidor === "object") {
            for (const key in errorServidor) {
              if (errorServidor[key]) {
                modalStateErrors += errorServidor[key] + "\n";
              }
            }
          }

          return throwError(
            modalStateErrors || errorServidor || "Error del servidor"
          );
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};

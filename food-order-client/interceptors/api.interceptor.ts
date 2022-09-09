import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const jwttoken = localStorage.getItem('jwttoken');
    if (jwttoken != null) {
      req = req.clone({
        setHeaders: { Authorization: `Bearer ${jwttoken}` },
      });
    }
    return next.handle(req).pipe(
      tap(
        (x) => x,
        (error) => {
          console.log(error);
        }
      )
    );
  }
}

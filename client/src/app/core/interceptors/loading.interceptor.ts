import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
 
  constructor(private busyService:  BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if (request.method === 'GET' && request.url.includes('dashboard')) {
      return next.handle(request);
    }
    if (request.method === 'POST' && request.url.includes('dashboard') && request.url.includes('GetSmsRecap')) {
      return next.handle(request);
    }
    if (request.url.includes('emailexists')) {
      return next.handle(request);
    }

    if (request.url.includes('NotificationThread')) {
      return next.handle(request);
    }

    this.busyService.busy();
    return next.handle(request).pipe(
      delay(500),
       finalize(() => {
        this.busyService.idle()
      })
    );
  }
}

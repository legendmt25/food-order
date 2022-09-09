import { Injectable } from '@angular/core';
import { LoginDto, RegisterDto } from 'generated/models';
import { AuthService } from 'generated/services';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CustomAuthService extends AuthService {
  private jwttoken = new ReplaySubject<string | null>(1);

  get jwtObservable() {
    return this.jwttoken.asObservable();
  }

  login(body: LoginDto) {
    return this.authPost$Json({ body }).pipe(
      map((value) => {
        localStorage.setItem('jwttoken', value);
        this.jwttoken.next(value);
      })
    );
  }

  init() {
    this.jwttoken.next(localStorage.getItem('jwttoken'));
  }

  register(body: RegisterDto) {
    return this.authRegisterPost({ body });
  }

  logout() {
    localStorage.removeItem('jwttoken');
    this.jwttoken.next(null);
  }
}

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from 'generated/models';
import { AuthService } from 'services';

@Component({
  selector: 'login',
  templateUrl: './login.html',
})
export class LoginComponent {
  loginData: LoginDto = { username: '', password: '' };
  constructor(private authService: AuthService, private router: Router) {}

  handleLogin() {
    this.authService.login(this.loginData).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}

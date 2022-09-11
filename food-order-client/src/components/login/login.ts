import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from 'generated/models';
import { AuthService } from 'services';

@Component({
  selector: 'login',
  templateUrl: './login.html',
})
export class LoginComponent {
  inputClass =
    'outline-none border rounded p-1 px-2 shadow-sm hover:border-zinc-300 focus:border-zinc-300 transition-all duration-200 w-full';
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

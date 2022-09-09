import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterDto } from 'generated/models';
import { AuthService } from 'services';

@Component({
  selector: 'register',
  templateUrl: './register.html',
})
export class RegisterComponent {
  inputClass =
    'outline-none border rounded p-1 px-2 shadow-sm hover:border-zinc-300 focus:border-zinc-300 transition-all duration-200';
  registerData: RegisterDto & { confirmPassword: string | null } = {
    username: '',
    password: '',
    email: '',
    firstName: '',
    lastName: '',
    confirmPassword: '',
  };
  constructor(private authService: AuthService, private router: Router) {}

  handleRegister() {
    if (this.registerData.password !== this.registerData.confirmPassword) {
      alert('Passwords are not the same!');
      return;
    }
    this.authService.register(this.registerData).subscribe({
      next: () => {
        console.log('Registered');
        this.router.navigate(['/login']);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}

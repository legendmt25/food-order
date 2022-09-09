import { Component, OnInit } from '@angular/core';
import { AuthService } from 'services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  showCartModal: boolean = false;
  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.authService.init();
  }

  handleToggleCartModal(): void {
    this.showCartModal = !this.showCartModal;
  }
  
  handleLogout(): void {
    this.authService.logout();
  }
}

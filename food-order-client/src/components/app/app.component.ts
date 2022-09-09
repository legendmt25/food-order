import { Component, OnInit } from '@angular/core';
import { AuthService } from 'services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.authService.init();
  }

  handleLogout(): void {
    this.authService.logout();
  }
}

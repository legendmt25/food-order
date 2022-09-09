import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'navbar',
  templateUrl: './nav.html',
})
export class NavbarComponent {
  @Input('isAuthenticated') isAuthenticated: boolean = false;
  @Output('onLogout') onLogout = new EventEmitter<string>();
  @Input('showCartModal') showCartModal: boolean = false;
  @Output('onToggleCartModal') onToggleCartModal = new EventEmitter();
  linkClass =
    'text-red-400 border-b-zinc-400 p-2 px-6 hover:border-b-zinc-600 hover:text-red-600 rounded transition-all duration-300 ease-in-out';

  handleToggleCartModal() {
    this.onToggleCartModal.emit();
  }

  handleLogout() {
    this.onLogout.emit();
  }
}

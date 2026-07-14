import { Component, output } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: true,
  styleUrl: './navbar.css',
  templateUrl: './navbar.html'
})
export class Navbar {
  logout = output<void>();
}

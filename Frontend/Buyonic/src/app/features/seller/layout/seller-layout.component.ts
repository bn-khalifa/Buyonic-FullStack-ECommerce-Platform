import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-seller-layout',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent],
  template: `
    <app-navbar />
    <main class="seller-main">
      <router-outlet />
    </main>
  `,
  styles: [`.seller-main { min-height: calc(100vh - 64px); background: var(--bg-primary); }`]
})
export class SellerLayoutComponent {}

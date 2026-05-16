import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-customer-layout',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent],
  template: `
    <app-navbar />
    <main class="customer-main">
      <router-outlet />
    </main>
  `,
  styles: [`
    .customer-main {
      min-height: calc(100vh - 64px);
      background: var(--bg-primary);
    }
  `]
})
export class CustomerLayoutComponent {}

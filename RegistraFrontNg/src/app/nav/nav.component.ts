import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Loging successful')
    }, err => {
      console.log('Error');
    }, () => {
      this.router.navigate(['/companies-list']);
    });
  }
  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
    this.router.navigate(['/home']);
  }
}

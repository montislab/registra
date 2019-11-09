import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl + 'admin/';

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'getUsersWithRoles');
  }

  updateUserRoles(user: User, roles) {
    return this.http.post(this.baseUrl + 'editRoles', {userName: user.userName, roleNames: roles.rolesNames});
  }

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'http://localhost:5000/'; // TODO: it should be changed to environment.apiUrl also in other places

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'admin/getUsersWithRoles');
  }

  updateUserRoles(user: User, roles) {
    return this.http.post(this.baseUrl + 'admin/editRoles', {userName: user.userName, roleNames: roles.rolesNames});
  }

}

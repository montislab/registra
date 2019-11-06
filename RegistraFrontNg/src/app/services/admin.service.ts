import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'http://localhost:5000/'; // TODO: it should be changed to environment.apiUrl

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'admin/getUsersWithRoles');
  }

}

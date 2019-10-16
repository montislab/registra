import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-all-client-list',
  templateUrl: './all-client-list.component.html',
  styleUrls: ['./all-client-list.component.css']
})
export class AllClientListComponent implements OnInit {
  clients: Client[];
  
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getAllClients();
  }

  getAllClients() {
    this.http.get<Client[]>('http://localhost:5000/Client').subscribe(response => {
      this.clients = response;
    }, error => {console.log(error)});
  }

}

interface Client {
  firstName: string;
  lastName: string;
  phoneNumber: string;
}

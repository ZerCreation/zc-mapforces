import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from "@aspnet/signalr";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private hubConnection: signalR.HubConnection

  constructor(private httpClient: HttpClient) { }

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:59816/game')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public joinToGame() {
    return this.httpClient.post("http://localhost:59816/api/game/join", null);
  }

}

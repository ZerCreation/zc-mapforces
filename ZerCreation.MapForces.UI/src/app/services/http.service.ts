import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { MapUnit } from '../dtos/map-unit';
import { Player } from '../dtos/player';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private hubConnection: signalR.HubConnection
  public positionChanged: EventEmitter<MapUnit> = new EventEmitter();

  constructor(private httpClient: HttpClient) { }

  // TODO: Move it to separated service
  public startHubConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.webApiUrl}/gamehub`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addHubListeners = () => {
    this.hubConnection.on('positionChangedNotification', (mapUnit) => {
      this.positionChanged.next(mapUnit);
    });

    this.hubConnection.on('nextPlayerTurnNotification', (player: Player) => {
      console.log(`New turn of ${player.color} player.`);
    });
  }

  public joinToGame() {
    return this.httpClient.post(`${environment.webApiUrl}/api/game/join`, null);
  }

}

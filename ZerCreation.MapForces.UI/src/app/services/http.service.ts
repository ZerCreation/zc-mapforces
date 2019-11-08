import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { MapUnit } from '../dtos/map-unit';
import { Player } from '../dtos/player';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private hubConnection: signalR.HubConnection
  public hubConnected: EventEmitter<void> = new EventEmitter();
  public positionChanged: EventEmitter<MapUnit> = new EventEmitter();
  public movingPlayerChanged: EventEmitter<Player> = new EventEmitter();
  public roundIdChanged: EventEmitter<number> = new EventEmitter();

  constructor(private httpClient: HttpClient) { }

  // TODO: Move it to separated service
  public startHubConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.webApiUrl}/gamehub`)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.hubConnected.next();
      })
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addHubListeners = () => {
    this.hubConnection.on('playerConnectedNotification', () => {
      console.log('Player connected');
    });

    this.hubConnection.on('positionChangedNotification', (mapUnit) => {
      this.positionChanged.next(mapUnit);
    });

    this.hubConnection.on('nextPlayerTurnNotification', (player: Player) => {
      this.movingPlayerChanged.next(player);
      console.log(`New turn of ${player.color} player.`);
    });

    this.hubConnection.on('roundIdUpdateNotification', (roundId: number) => {
      this.roundIdChanged.next(roundId);
    });

    this.hubConnection.on('generalNotification', (message: string) => {
      console.log(message);
    });
  }

  public joinToGame(): Observable<any> {
    return this.httpClient.post(`${environment.webApiUrl}/api/game/join`, null);
  }

  public async finishTurnAsync(): Promise<void> {
    await this.httpClient.put(`${environment.webApiUrl}/api/game/turn`, {})
      .toPromise();
  }
}

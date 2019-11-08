import { Injectable } from '@angular/core';
import { Player } from '../dtos/player';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  private players: Player[];
  public localPlayer: Player;
  
  constructor() { }

  public init(players: Player[], localPlayerId: string): void {
    this.players = players;
    this.localPlayer = players.find(_ => _.id === localPlayerId);
  }

  public getColorById(playerId: string): string {
    const player = this.players.find(player => player.id == playerId);
    if (player == null) {
      throw new Error(`Error in setting colors of players. Player with Id = ${playerId} is unknown.`)
    }

    return player.color;
  }
}

import { Injectable } from '@angular/core';
import { Player } from '../dtos/player';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  private players: Player[];
  public currentPlayer: Player;
  
  constructor() { }

  public init(players: Player[], currentPlayerId: string): void {
    this.players = players;
    this.currentPlayer = players.find(_ => _.id === currentPlayerId);
  }

  public getColorById(playerId: string): string {
    const player = this.players.find(player => player.id == playerId);
    if (player == null) {
      throw new Error(`Error in setting colors of players. Player with Id = ${playerId} is unknown.`)
    }

    return player.color;
  }
}

import { Injectable } from '@angular/core';
import { Player } from '../dtos/player';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  private players: Player[];
  public currentPlayer: Player;
  
  constructor() { }

  public init(players: Player[]): void {
    this.players = players;

    // Temporary
    this.currentPlayer = players[0];
  }

  public getColorByPlayerId(playerId: string): string {
    const player = this.players.find(player => player.id == playerId);
    if (player == null) {
      throw new Error(`Error in setting colors of players. Player with Id = ${playerId} is unknown.`)
    }

    return player.color;
  }
}

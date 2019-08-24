import { MapUnit } from './map-unit';
import { Player } from './player';

export interface GamePlayDetails {
    mapWidth: number;
    mapHeight: number;
    units: MapUnit[];
    players: Player[];
}

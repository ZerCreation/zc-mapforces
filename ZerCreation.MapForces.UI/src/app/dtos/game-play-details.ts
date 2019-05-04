import { MapUnit } from './map-unit';

export interface GamePlayDetails {
    mapWidth: number;
    mapHeight: number;
    units: MapUnit[];
}

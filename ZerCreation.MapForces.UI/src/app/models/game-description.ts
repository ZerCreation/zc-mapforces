import { TerrainUnit } from './terrain-unit';

export interface GameDescription {
    mapWidth: number;
    mapHeight: number;
    terrainUnits: TerrainUnit[];
}

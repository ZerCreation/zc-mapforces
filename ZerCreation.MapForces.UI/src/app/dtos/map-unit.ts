import { Ownership } from './ownership';

export interface MapUnit {
    x: number;
    y: number;
    terrainType: string;
    ownership: Ownership;
}

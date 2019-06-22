import { PlayerDto } from './player-dto';

export interface MapUnit {
    x: number;
    y: number;
    terrainType: string;
    ownedBy: PlayerDto;
}

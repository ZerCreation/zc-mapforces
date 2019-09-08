import { Injectable } from '@angular/core';
import { MapUnit } from '../dtos/map-unit';
import { MapViewUnit } from '../models/map-view-unit';
import { Ownership } from '../dtos/ownership';
import { PlayersService } from './players.service';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  public units: MapViewUnit[];
  public unitSize = 12;
  public unitSizeWithMargin = this.unitSize + 1;

  constructor(private playersService: PlayersService) { }

  public createMapViewUnits(mapUnits: MapUnit[]): void {
    this.units = mapUnits.map(unit => {
      return {
        x: unit.x * this.unitSizeWithMargin,
        y: unit.y * this.unitSizeWithMargin,
        color: this.selectColor(unit),
        canBeSelected: this.isUnitOfCurrentPlayer(unit.ownership)
      };
    })
  }

  public findUnitToSelectByCoordinates(x: number, y: number) {
    return this.units.find(unit => 
      unit.canBeSelected &&
      (x >= unit.x && x <= unit.x + this.unitSizeWithMargin) &&
      (y >= unit.y && y <= unit.y + this.unitSizeWithMargin));
  }

  public findUnitByCoordinates(x: number, y: number): MapViewUnit {
    return this.units.find(unit => 
      (x >= unit.x && x <= unit.x + this.unitSizeWithMargin) &&
      (y >= unit.y && y <= unit.y + this.unitSizeWithMargin));
  }

  public updateMapViewUnit(mapUnit: MapUnit): MapViewUnit {
    const mapViewUnit = this.findMapViewUnit(mapUnit);
    mapViewUnit.color = this.selectColor(mapUnit);
    mapViewUnit.canBeSelected = this.isUnitOfCurrentPlayer(mapUnit.ownership);

    return mapViewUnit;
  }

  public getMapUnitCoordinates(unit: MapViewUnit) {
    const logicX = unit.x / this.unitSizeWithMargin;
    const logicY = unit.y / this.unitSizeWithMargin;

    return { x: logicX, y: logicY };
  }

  private selectColor(unit: MapUnit): string {
    if (unit.ownership != null) {
      return this.playersService.getColorByPlayerId(unit.ownership.playerId);
    }

    if (unit.terrainType == 'Earth') {
      return 'gray';
    }
    
    return 'white';
  }

  private isUnitOfCurrentPlayer(ownership: Ownership): any {
    // TODO: Recognize players here
    return ownership != null;
  }

  private findMapViewUnit(mapUnit: MapUnit): MapViewUnit {
    return this.units.find(unit => 
      (mapUnit.x >= unit.x / this.unitSizeWithMargin && mapUnit.x <= (unit.x + 1) / this.unitSizeWithMargin) &&
      (mapUnit.y >= unit.y / this.unitSizeWithMargin && mapUnit.y <= (unit.y + 1) / this.unitSizeWithMargin));
  }
}

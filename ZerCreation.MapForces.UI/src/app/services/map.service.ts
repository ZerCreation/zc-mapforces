import { Injectable } from '@angular/core';
import { MapUnit } from '../dtos/map-unit';
import { MapViewUnit } from '../models/map-view-unit';
import { Ownership } from '../dtos/ownership';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  public units: MapViewUnit[];
  public unitSize = 12;
  public unitSizeWithMargin = this.unitSize + 1;

  constructor() { }

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

  public findUnitByCoordinates(x: number, y: number) {
    return this.units.find(unit => 
      (x >= unit.x && x <= unit.x + this.unitSizeWithMargin) &&
      (y >= unit.y && y <= unit.y + this.unitSizeWithMargin));
  }

  public getLogicCoordinatesOfUnit(unit: MapViewUnit) {
    const logicX = unit.x / this.unitSizeWithMargin;
    const logicY = unit.y / this.unitSizeWithMargin;

    return { x: logicX, y: logicY };
  }

  private selectColor(unit: MapUnit): string {
    if (unit.ownership != null) {
      return 'orange';
    }

    if (unit.terrainType == 'Earth') {
      return 'green';
    }
    
    return 'blue';
  }

  private isUnitOfCurrentPlayer(ownership: Ownership): any {
    // TODO: Recognize players here
    return ownership != null;
  }
}

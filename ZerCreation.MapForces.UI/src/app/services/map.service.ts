import { Injectable } from '@angular/core';
import { MapUnit } from '../dtos/map-unit';
import { MapViewUnit } from '../models/map-view-unit';

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
        color: this.selectColor(unit)
      };
    })
  }


  private selectColor(unit: MapUnit): string {
    if (unit.ownedBy != null) {
      return 'orange';
    }

    if (unit.terrainType == 'Earth') {
      return 'green';
    }
    
    return 'blue';
  }
}

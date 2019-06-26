import { Injectable } from '@angular/core';
import { MapViewUnit } from '../models/map-view-unit';
import { MapService } from './map.service';

@Injectable({
  providedIn: 'root'
})
export class MoveService {

  constructor(private mapService: MapService) { }
  
  moveSelectedTo(selectedUnits: MapViewUnit[], mouseX: number, mouseY: number): boolean {
    if (selectedUnits.length == 0) {
      return false;
    }

    var unit: MapViewUnit = this.mapService.findUnitByCoordinates(mouseX, mouseY);
    if (unit != null) {
      // TODO: Implement move here
      const { x, y } = this.mapService.getLogicCoordinatesOfUnit(unit);
      alert(`Move attempt to (${x}, ${y})`);
      return true;
    }

    return false;
  }

}

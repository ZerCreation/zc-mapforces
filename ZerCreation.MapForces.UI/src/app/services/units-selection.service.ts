import { Injectable } from '@angular/core';
import { MapViewUnit } from '../models/map-view-unit';
import { MapService } from './map.service';

@Injectable({
  providedIn: 'root'
})
export class UnitsSelectionService {

  private selectedUnits: MapViewUnit[] = [];
  constructor(private mapService: MapService) { }

  updateUnitsSelection(mouseX: number, mouseY: number): MapViewUnit[] {
    // TODO: Define more selection strategies here

    var unitToAdd: MapViewUnit = this.mapService.units.find(unit => 
      unit.canBeSelected &&
      (mouseX >= unit.x && mouseX <= unit.x + this.mapService.unitSizeWithMargin) &&
      (mouseY >= unit.y && mouseY <= unit.y + this.mapService.unitSizeWithMargin));

    if (unitToAdd != null && !this.selectedUnits.includes(unitToAdd)) {
      this.selectedUnits.push(unitToAdd);
    }

    return this.selectedUnits;
  }

}

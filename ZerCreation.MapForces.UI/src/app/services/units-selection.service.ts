import { Injectable } from '@angular/core';
import { MapViewUnit } from '../models/map-view-unit';
import { MapService } from './map.service';

@Injectable({
  providedIn: 'root'
})
export class UnitsSelectionService {  
  private _selectedUnits: MapViewUnit[];
  get units(): MapViewUnit[] {
    return this._selectedUnits;
  }

  constructor(private mapService: MapService) {
    this._selectedUnits = [];
  }

  updateUnitsSelection(mouseX: number, mouseY: number) {
    // TODO: Define more selection strategies here

    var unitToAdd: MapViewUnit = this.mapService.findUnitToSelectByCoordinates(mouseX, mouseY);

    if (unitToAdd != null && !this._selectedUnits.includes(unitToAdd)) {
      this._selectedUnits.push(unitToAdd);
    }
  }

  clearSelection() {
    this._selectedUnits = [];
  }
}

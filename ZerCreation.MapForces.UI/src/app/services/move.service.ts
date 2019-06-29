import { Injectable } from '@angular/core';
import { MapViewUnit } from '../models/map-view-unit';
import { MapService } from './map.service';
import { HttpClient } from '@angular/common/http';
import { MoveDto } from '../dtos/move-dto';
import { MapUnit } from '../dtos/map-unit';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MoveService {

  constructor(
    private mapService: MapService,
    private httpClient: HttpClient) { }
  
  moveSelectedTo(selectedUnits: MapViewUnit[], mouseX: number, mouseY: number): boolean {
    if (selectedUnits.length == 0) {
      return false;
    }

    var targetCenterUnit: MapViewUnit = this.mapService.findUnitByCoordinates(mouseX, mouseY);
    if (targetCenterUnit != null) {
      const { x: moveX, y: moveY } = this.mapService.getMapUnitCoordinates(selectedUnits[0]);
      const { x: targetX, y: targetY } = this.mapService.getMapUnitCoordinates(targetCenterUnit);
      let moveDto: MoveDto = {
        unitsToMove: [{ x: moveX, y: moveY }],
        unitsTarget: [{ x: targetX, y: targetY }]
      };

      this.httpClient.post(`${environment.webApiUrl}/api/game/move`, moveDto)
        .subscribe(() => { });
      return true;
    }

    return false;
  }

}

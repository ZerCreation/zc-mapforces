import { Injectable } from '@angular/core';
import { MapViewUnit } from '../models/map-view-unit';
import { MapService } from './map.service';
import { HttpClient } from '@angular/common/http';
import { MoveDto } from '../dtos/move-dto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MoveService {

  constructor(
    private mapService: MapService,
    private httpClient: HttpClient) { }
  
  public async moveSelectedTo(selectedUnits: MapViewUnit[], mouseX: number, mouseY: number): Promise<boolean> {
    var targetCenterUnit: MapViewUnit = this.mapService.findUnitByCoordinates(mouseX, mouseY);
    if (targetCenterUnit != null) {
      const { x: moveX, y: moveY } = this.mapService.getMapUnitCoordinates(selectedUnits[0]);
      const { x: targetX, y: targetY } = this.mapService.getMapUnitCoordinates(targetCenterUnit);
      let moveDto: MoveDto = {
        unitsToMove: [{ x: moveX, y: moveY }],
        unitsTarget: [{ x: targetX, y: targetY }]
      };

      await this.httpClient
        .post(`${environment.webApiUrl}/api/game/move`, moveDto)
        .toPromise();
        
      return true;
    }

    return false;
  }

}

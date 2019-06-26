import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { GamePlayDetails } from 'src/app/dtos/game-play-details';
import { MapUnit } from 'src/app/dtos/map-unit';
import { map, tap } from 'rxjs/operators';
import { MapService } from 'src/app/services/map.service';
import { MapViewUnit } from 'src/app/models/map-view-unit';
import { UnitsSelectionService } from 'src/app/services/units-selection.service';
import { MoveService } from 'src/app/services/move.service';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent implements OnInit, AfterViewInit {
  @ViewChild('canvas') canvas: ElementRef;
  private htmlCanvas: HTMLCanvasElement;
  private context: CanvasRenderingContext2D;

  constructor(
    private httpService: HttpService,
    private mapService: MapService,
    private unitsSelectionService: UnitsSelectionService,
    private moveService: MoveService) {}

  ngOnInit() {
    this.htmlCanvas = this.canvas.nativeElement as HTMLCanvasElement;

    this.httpService.joinToGame().pipe(
      tap((data: GamePlayDetails) => this.drawBackground(data.mapWidth, data.mapHeight)),
      map(data => data.units),
      tap((units: MapUnit[]) => this.mapService.createMapViewUnits(units)))
      .subscribe(() => {
        this.drawManyUnits(this.mapService.units);
      });
  }

  ngAfterViewInit() {
    this.context = this.htmlCanvas.getContext('2d');
  }

  private drawBackground(width: number, height: number) {   
    this.htmlCanvas.width = width * this.mapService.unitSizeWithMargin - 1;
    this.htmlCanvas.height = height * this.mapService.unitSizeWithMargin - 1;

    this.context.fillStyle = 'lightgray';
    for (let x = 0; x < this.htmlCanvas.width; x += this.mapService.unitSizeWithMargin) {
      for (let y = 0; y < this.htmlCanvas.height; y += this.mapService.unitSizeWithMargin) {
        this.context.fillRect(x, y, this.mapService.unitSize, this.mapService.unitSize);
      }
    }
  }

  public onCanvasClicked(event: MouseEvent) {
    
    const { mouseX, mouseY } = this.getMouseCoordinates(event);

    // Try to move
    var moveWasDone: boolean = this.moveService.moveSelectedTo(this.unitsSelectionService.units, mouseX, mouseY);
    if (moveWasDone) {
      this.drawManyUnits(this.unitsSelectionService.units);
      this.unitsSelectionService.clearSelection();
      return;
    }

    // Get then mark currently selected units
    this.unitsSelectionService.updateUnitsSelection(mouseX, mouseY);
    this.drawManyUnits(this.unitsSelectionService.units, "black");
  }

  private getMouseCoordinates(event: MouseEvent) {
    var canvasRectangle = this.htmlCanvas.getBoundingClientRect();
    const mouseX = event.clientX - canvasRectangle.left;
    const mouseY = event.clientY - canvasRectangle.top;

    return { mouseX, mouseY };
  }

  private drawManyUnits(units: MapViewUnit[], customColor: string = null) {
    units.forEach(unit => this.drawUnit(unit, customColor));
  }

  private drawUnit(unit: MapViewUnit, customColor: string = null) {
    this.context.fillStyle = customColor != null ? customColor : unit.color;
    this.context.fillRect(unit.x, unit.y, this.mapService.unitSize, this.mapService.unitSize);
  }
}

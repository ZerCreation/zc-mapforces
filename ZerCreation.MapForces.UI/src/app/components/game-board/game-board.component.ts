import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { GamePlayDetails } from 'src/app/dtos/game-play-details';
import { MapUnit } from 'src/app/dtos/map-unit';
import { map, tap } from 'rxjs/operators';
import { MapService } from 'src/app/services/map.service';
import { MapViewUnit } from 'src/app/models/map-view-unit';
import { UnitsSelectionService } from 'src/app/services/units-selection.service';
import { MoveService } from 'src/app/services/move.service';
import { PlayersService } from 'src/app/services/players.service';

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
    private moveService: MoveService,
    private playersService: PlayersService) {}

  ngOnInit() {
    this.htmlCanvas = this.canvas.nativeElement as HTMLCanvasElement;

    this.httpService.startHubConnection();
    this.httpService.addHubListener();

    this.httpService.joinToGame().pipe(
      tap((data: GamePlayDetails) => this.drawBackground(data.mapWidth, data.mapHeight)),
      tap(data => this.playersService.init(data.players, data.newPlayerId)),
      map(data => data.units),
      tap((units: MapUnit[]) => this.mapService.createMapViewUnits(units)))
      .subscribe(() => {
        this.drawManyUnits(this.mapService.units);
      });

    this.httpService.positionChanged
      .subscribe(mapUnit => {
        // const mapUnits: MapUnit[] = [ mapUnit ];
        // Update unit's ownership and color
        const mapViewUnit = this.mapService.updateMapViewUnit(mapUnit);
        this.drawUnit(mapViewUnit, this.playersService.currentPlayer.color);
      });
  }

  ngAfterViewInit() {
    this.context = this.htmlCanvas.getContext('2d');
  }

  private drawBackground(width: number, height: number) {   
    this.htmlCanvas.width = width * this.mapService.unitSizeWithMargin - 1;
    this.htmlCanvas.height = height * this.mapService.unitSizeWithMargin - 1;

    this.context.fillStyle = 'white';
    for (let x = 0; x < this.htmlCanvas.width; x += this.mapService.unitSizeWithMargin) {
      for (let y = 0; y < this.htmlCanvas.height; y += this.mapService.unitSizeWithMargin) {
        this.context.fillRect(x, y, this.mapService.unitSize, this.mapService.unitSize);
      }
    }
  }

  public async onCanvasClicked(event: MouseEvent) {
    const { mouseX, mouseY } = this.getMouseCoordinates(event);
    const selectedUnits = this.unitsSelectionService.units;

    if (selectedUnits.length == 0) {
      this.selectPlayerUnits(selectedUnits, mouseX, mouseY);
    } else {
      await this.moveSelectedUnits(selectedUnits, mouseX, mouseY);
    }
  }

  public onFinishTurnButtonClicked() {
    console.log('Turn finish button was clicked.');
  }

  private selectPlayerUnits(selectedUnits: MapViewUnit[], mouseX: number, mouseY: number) {
    this.unitsSelectionService.updateUnitsSelection(mouseX, mouseY);
    this.drawManyUnits(selectedUnits, 'black');
  }

  private async moveSelectedUnits(selectedUnits: MapViewUnit[], mouseX: number, mouseY: number) {
    var moveWasDone: boolean = await this.moveService.moveSelectedTo(selectedUnits, mouseX, mouseY);
    if (moveWasDone) {
      console.log(`Moved to requested (${mouseX}, ${mouseY}) mouse coordinates.`);
      this.drawManyUnits(selectedUnits);
      this.unitsSelectionService.clearSelection();
    }
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

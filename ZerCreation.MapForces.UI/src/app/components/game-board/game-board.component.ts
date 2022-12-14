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
import { Player } from 'src/app/dtos/player';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent implements OnInit, AfterViewInit {
  @ViewChild('canvas') canvas: ElementRef;
  private htmlCanvas: HTMLCanvasElement;
  private context: CanvasRenderingContext2D;

  // TODO: Move it to player's info
  public localPlayer: Player;
  public movingPlayer: Player;
  public roundId: number;

  constructor(
    private httpService: HttpService,
    private mapService: MapService,
    private unitsSelectionService: UnitsSelectionService,
    private moveService: MoveService,
    private playersService: PlayersService) {}

  ngOnInit() {
    this.htmlCanvas = this.canvas.nativeElement as HTMLCanvasElement;

    this.httpService.startHubConnection();
    this.httpService.addHubListeners();

    this.httpService.hubConnected.subscribe(() => {
      this.httpService.joinToGame().pipe(
        tap((data: GamePlayDetails) => this.drawBackground(data.mapWidth, data.mapHeight)),
        tap(data => this.playersService.init(data.players, data.newPlayerId)),
        map(data => data.units),
        tap((units: MapUnit[]) => this.mapService.createMapViewUnits(units)))
        .subscribe(() => {
          this.drawManyUnits(this.mapService.units);
          this.localPlayer = this.playersService.localPlayer;
        });      
    });

    this.httpService.positionChanged
      .subscribe(mapUnit => {
        // const mapUnits: MapUnit[] = [ mapUnit ];
        // Update unit's ownership and color
        const mapViewUnit = this.mapService.updateMapViewUnit(mapUnit);
        this.drawUnit(mapViewUnit);
      });

    this.httpService.movingPlayerChanged.subscribe(player => this.movingPlayer = player);
    this.httpService.roundIdChanged.subscribe(id => this.roundId = id);
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
    if (!this.isLocalPlayerTurn()) {
      return;
    }

    const { mouseX, mouseY } = this.getMouseCoordinates(event);
    const selectedUnits = this.unitsSelectionService.units;

    if (selectedUnits.length === 0) {
      this.selectPlayerUnits(selectedUnits, mouseX, mouseY);
    } else {
      await this.moveSelectedUnits(selectedUnits, mouseX, mouseY);
    }
  }

  public async onFinishTurnButtonClicked() {
    if (this.isLocalPlayerTurn()) {
      await this.httpService.finishTurnAsync();
    }
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

  private isLocalPlayerTurn(): boolean {
    return this.localPlayer.id === this.movingPlayer.id;
  }
}

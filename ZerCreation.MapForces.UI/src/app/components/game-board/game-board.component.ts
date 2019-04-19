import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { GamePlayDetails } from 'src/app/models/game-play-details';
import { MapUnit } from 'src/app/models/map-unit';
import { map, tap } from 'rxjs/operators';
import { MapService } from 'src/app/services/map.service';

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
    private mapService: MapService) {}

  ngOnInit() {
    this.htmlCanvas = this.canvas.nativeElement as HTMLCanvasElement;

    this.httpService.joinToGame().pipe(
      tap((data: GamePlayDetails) => this.drawBackground(data.mapWidth, data.mapHeight)),
      map(data => data.units),
      tap((units: MapUnit[]) => this.mapService.createMapViewUnits(units)))
      .subscribe(() => {
        this.drawMap();
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

  private drawMap() {
    this.mapService.units.forEach(unit => {
      this.context.fillStyle = unit.color;
      this.context.fillRect(unit.x, unit.y, this.mapService.unitSize, this.mapService.unitSize);
    });
  }

  public onCanvasClicked(event: MouseEvent) {
    var canvasRectangle = this.htmlCanvas.getBoundingClientRect();
    const mouseX = event.clientX - canvasRectangle.left;
    const mouseY = (event.clientY - canvasRectangle.top);

    var selectedUnit = this.mapService.units.find(unit => 
      (mouseX >= unit.x && mouseX <= unit.x + this.mapService.unitSize) &&
      (mouseY >= unit.y && mouseY <= unit.y + this.mapService.unitSize));

    if (selectedUnit != null) {
      this.context.fillStyle = 'black';
      this.context.fillRect(selectedUnit.x, selectedUnit.y, this.mapService.unitSize, this.mapService.unitSize);
    }
  }
}

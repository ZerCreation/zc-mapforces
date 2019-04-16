import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { GamePlayDetails } from 'src/app/models/game-play-details';
import { MapUnit } from 'src/app/models/map-unit';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent implements OnInit, AfterViewInit {
  @ViewChild('canvas') canvas: ElementRef;
  private htmlCanvas: HTMLCanvasElement;
  private context: CanvasRenderingContext2D;

  private unitSize = 9;
  private unitSizeWithMargin = this.unitSize + 1;

  constructor(private httpService: HttpService) {}

  ngOnInit() {
    this.htmlCanvas = this.canvas.nativeElement as HTMLCanvasElement;

    this.httpService.joinToGame()
      .subscribe((data: GamePlayDetails) => {
        this.drawBackground(data.mapWidth, data.mapHeight);
        this.drawMap(data.units);
      });
  }

  ngAfterViewInit() {
    this.context = this.htmlCanvas.getContext('2d');
  }

  private drawBackground(width: number, height: number) {   
    this.htmlCanvas.width = width * this.unitSizeWithMargin - 1;
    this.htmlCanvas.height = height * this.unitSizeWithMargin - 1;

    this.context.fillStyle = 'lightgray';

    for (let x = 0; x < this.htmlCanvas.width; x += this.unitSizeWithMargin) {
      for (let y = 0; y < this.htmlCanvas.height; y += this.unitSizeWithMargin) {
        this.context.fillRect(x, y, this.unitSize, this.unitSize);
      }
    }
  }

  drawMap(units: MapUnit[]) {
    
    units.forEach(unit => {
      this.context.fillStyle = unit.terrainType == 'Earth' ? 'green' : 'blue';
      this.context.fillRect(this.unitSizeWithMargin * unit.x, this.unitSizeWithMargin * unit.y, this.unitSize, this.unitSize);
    });
  }
}

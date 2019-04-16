import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { GameDescription } from 'src/app/models/game-description';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent implements OnInit, AfterViewInit {
  @ViewChild('canvas') canvas: ElementRef;
  private htmlCanvas: HTMLCanvasElement;
  private context: CanvasRenderingContext2D;
  private mapWidth: number;
  private mapHeight: number;

  constructor(private httpService: HttpService) {}

  ngOnInit() {
    this.htmlCanvas = this.canvas.nativeElement as HTMLCanvasElement;

    this.httpService.joinToGame()
      .subscribe((data: GameDescription) => {
        this.mapWidth = data.mapWidth;
        this.mapHeight = data.mapHeight;

        this.draw();
      });
  }

  ngAfterViewInit() {
    this.context = this.htmlCanvas.getContext('2d');
  
  }

  private draw() {
    const unitSize = 9;
    const unitSizeWithMargin = unitSize + 1;
    
    this.htmlCanvas.width = this.mapWidth * unitSizeWithMargin;
    this.htmlCanvas.height = this.mapHeight * unitSizeWithMargin;

    this.context.fillStyle = 'green';

    for (let x = 0; x < this.htmlCanvas.width; x += unitSizeWithMargin) {
      for (let y = 0; y < this.htmlCanvas.height; y += unitSizeWithMargin) {
        this.context.fillRect(x, y, unitSize, unitSize);
      }
    }
  }
}

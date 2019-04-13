import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent implements AfterViewInit {
  @ViewChild('canvas') canvasEl: ElementRef;
  private context: CanvasRenderingContext2D;

  constructor() {}

  ngAfterViewInit() {
    this.context = (this.canvasEl.nativeElement as HTMLCanvasElement).getContext('2d');
  
    this.draw();
  }

  private draw() {
    this.context.fillStyle = 'green';

    for (let x = 0; x < 800; x+=10) {
      for (let y = 0; y < 800; y+=10) {
        this.context.fillRect(x, y, 9, 9);
      }
    }
  }

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpClient: HttpClient) { }

  public joinToGame() {
    return this.httpClient.post("http://localhost:59816/api/game/join", null);
  }

}

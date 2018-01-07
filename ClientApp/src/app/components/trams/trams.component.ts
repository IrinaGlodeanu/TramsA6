import { Component, OnInit } from '@angular/core';

import { Tram } from '../../_services/tram/tram';
import { TramService } from "../../_services/tram/tram.service";

@Component({
  selector: 'app-trams',
  templateUrl: './trams.component.html',
  styleUrls: ['./trams.component.css']
})
export class TramsComponent implements OnInit {

  trams: Tram[];

  constructor(private tramService: TramService) { }

  ngOnInit() {
    this.getTrams();
  }

  getTrams(): void {
    this.tramService.getTrams()
        .subscribe(trams => this.trams = trams);
  }
}
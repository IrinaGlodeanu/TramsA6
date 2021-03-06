import { Component, OnInit } from '@angular/core';
import { Tram } from '../../_services/tram/tram';
import { TramService } from '../../_services/tram/tram.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: [ './dashboard.component.css' ]
})
export class DashboardComponent implements OnInit {
  trams: Tram[] = [];

  constructor(private tramService: TramService) { }

  ngOnInit() {
    this.getTrams();
  }

  getTrams(): void {
    this.tramService.getTrams()
      .subscribe(trams => this.trams = trams.slice(0, 6));
  }
}

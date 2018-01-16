import { Component, OnInit, Input } from '@angular/core';
import { Tram } from '../../_services/tram/tram';
import { RunningTram } from '../../_services/tram/running-tram';

import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { TramService } from '../../_services/tram/tram.service';
import { RunningTramService } from '../../_services/tram/running-tram.service';

@Component({
  selector: 'app-tram-detail',
  templateUrl: './tram-detail.component.html',
  styleUrls: ['./tram-detail.component.css']
})
export class TramDetailComponent implements OnInit {
  @Input() tram: Tram;

  @Input() runningTram: RunningTram;

  constructor(
    private route: ActivatedRoute,
    private tramService: TramService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getTram();
  }

  getTram(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.tramService.getTram(id)
      .subscribe(tram => this.tram = tram);
  }

  goBack(): void {
    this.location.back();
  }
}

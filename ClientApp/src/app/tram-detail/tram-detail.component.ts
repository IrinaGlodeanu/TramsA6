import { Component, OnInit, Input } from '@angular/core';
import { Tram } from "../tram";

import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { TramService }  from '../tram.service';

@Component({
  selector: 'app-tram-detail',
  templateUrl: './tram-detail.component.html',
  styleUrls: ['./tram-detail.component.css']
})
export class TramDetailComponent implements OnInit {
  @Input() tram: Tram;

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

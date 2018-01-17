import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

import { RunningTram } from './running-tram';
import { RUNNINGTRAMS } from './mock-running-trams';

import { MessageService } from '../message/message.service';

@Injectable()
export class RunningTramService {

  constructor(private messageService: MessageService) { }

  getRunningTrams(): Observable<RunningTram[]>{
    // Todo: send the message _after_ fetching the trams
    this.messageService.add('TramService: fetched trams');
    return of(RUNNINGTRAMS);
}

  getRunningTram(id: number): Observable<RunningTram> {
    // Todo: send the message _after_ fetching the tram
    this.messageService.add(`TramService: fetched tram id=${id}`);
    return of(RUNNINGTRAMS.find(tram => tram.id === id));
  }
}

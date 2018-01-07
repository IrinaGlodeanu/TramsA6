import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

import { Tram } from './tram';
import { TRAMS } from './mock-trams';

import { MessageService } from '../message/message.service';

@Injectable()
export class TramService {

  constructor(private messageService: MessageService) { }

  getTrams(): Observable<Tram[]>{
    // Todo: send the message _after_ fetching the trams
    this.messageService.add('TramService: fetched trams');
    return of(TRAMS);
}

  getTram(id: number): Observable<Tram> {
    // Todo: send the message _after_ fetching the tram
    this.messageService.add(`TramService: fetched tram id=${id}`);
    return of(TRAMS.find(tram => tram.id === id));
  }
}

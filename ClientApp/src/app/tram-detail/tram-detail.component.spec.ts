import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TramDetailComponent } from './tram-detail.component';

describe('TramDetailComponent', () => {
  let component: TramDetailComponent;
  let fixture: ComponentFixture<TramDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TramDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TramDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

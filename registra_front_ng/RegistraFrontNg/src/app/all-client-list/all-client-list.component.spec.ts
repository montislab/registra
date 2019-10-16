import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllClientListComponent } from './all-client-list.component';

describe('AllClientListComponent', () => {
  let component: AllClientListComponent;
  let fixture: ComponentFixture<AllClientListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllClientListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllClientListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

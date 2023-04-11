import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleOtherDetailsComponent } from './sale-other-details.component';

describe('SaleOtherDetailsComponent', () => {
  let component: SaleOtherDetailsComponent;
  let fixture: ComponentFixture<SaleOtherDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SaleOtherDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SaleOtherDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

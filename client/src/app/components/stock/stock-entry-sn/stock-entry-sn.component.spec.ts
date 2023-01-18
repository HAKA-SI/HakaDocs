import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockEntrySnComponent } from './stock-entry-sn.component';

describe('StockEntrySnComponent', () => {
  let component: StockEntrySnComponent;
  let fixture: ComponentFixture<StockEntrySnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockEntrySnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockEntrySnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

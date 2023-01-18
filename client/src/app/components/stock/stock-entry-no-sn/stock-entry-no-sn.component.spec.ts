import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockEntryNoSnComponent } from './stock-entry-no-sn.component';

describe('StockEntryNoSnComponent', () => {
  let component: StockEntryNoSnComponent;
  let fixture: ComponentFixture<StockEntryNoSnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockEntryNoSnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockEntryNoSnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetProductWithSnModalComponent } from './set-product-with-sn-modal.component';

describe('SetProductWithSnModalComponent', () => {
  let component: SetProductWithSnModalComponent;
  let fixture: ComponentFixture<SetProductWithSnModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetProductWithSnModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SetProductWithSnModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

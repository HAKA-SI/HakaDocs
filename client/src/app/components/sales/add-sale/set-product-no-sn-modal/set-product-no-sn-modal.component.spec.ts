import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetProductNoSnModalComponent } from './set-product-no-sn-modal.component';

describe('SetProductNoSnModalComponent', () => {
  let component: SetProductNoSnModalComponent;
  let fixture: ComponentFixture<SetProductNoSnModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetProductNoSnModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SetProductNoSnModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

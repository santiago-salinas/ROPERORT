import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseCardComponent } from './purchase-card.component';

describe('PurchaseCardComponent', () => {
  let component: PurchaseCardComponent;
  let fixture: ComponentFixture<PurchaseCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PurchaseCardComponent]
    });
    fixture = TestBed.createComponent(PurchaseCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

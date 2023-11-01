import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductAdminCardComponent } from './product-admin-card.component';

describe('ProductAdminCardComponent', () => {
  let component: ProductAdminCardComponent;
  let fixture: ComponentFixture<ProductAdminCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductAdminCardComponent]
    });
    fixture = TestBed.createComponent(ProductAdminCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

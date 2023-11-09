import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersPurchasesComponent } from './users-purchases.component';

describe('UsersPurchasesComponent', () => {
  let component: UsersPurchasesComponent;
  let fixture: ComponentFixture<UsersPurchasesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UsersPurchasesComponent]
    });
    fixture = TestBed.createComponent(UsersPurchasesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

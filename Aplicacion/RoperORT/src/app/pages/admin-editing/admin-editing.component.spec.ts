import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminEditingComponent } from './admin-editing.component';

describe('AdminEditingComponent', () => {
  let component: AdminEditingComponent;
  let fixture: ComponentFixture<AdminEditingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminEditingComponent]
    });
    fixture = TestBed.createComponent(AdminEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

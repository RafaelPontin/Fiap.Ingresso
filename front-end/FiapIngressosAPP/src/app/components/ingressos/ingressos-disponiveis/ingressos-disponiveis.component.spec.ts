import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngressosDisponiveisComponent } from './ingressos-disponiveis.component';

describe('IngressosDisponiveisComponent', () => {
  let component: IngressosDisponiveisComponent;
  let fixture: ComponentFixture<IngressosDisponiveisComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IngressosDisponiveisComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IngressosDisponiveisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeusIngressosComponent } from './meus-ingressos.component';

describe('MeusIngressosComponent', () => {
  let component: MeusIngressosComponent;
  let fixture: ComponentFixture<MeusIngressosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MeusIngressosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MeusIngressosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

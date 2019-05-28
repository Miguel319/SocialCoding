/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CoderosListaComponent } from './coderos-lista.component';

describe('CoderosListaComponent', () => {
  let component: CoderosListaComponent;
  let fixture: ComponentFixture<CoderosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CoderosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CoderosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

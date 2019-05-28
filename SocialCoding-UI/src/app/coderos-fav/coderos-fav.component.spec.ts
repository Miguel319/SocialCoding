/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CoderosFavComponent } from './coderos-fav.component';

describe('CoderosFavComponent', () => {
  let component: CoderosFavComponent;
  let fixture: ComponentFixture<CoderosFavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CoderosFavComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CoderosFavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

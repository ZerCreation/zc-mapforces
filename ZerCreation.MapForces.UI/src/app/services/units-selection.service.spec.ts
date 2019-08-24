/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UnitsSelectionService } from './units-selection.service';

describe('Service: UnitsSelection', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UnitsSelectionService]
    });
  });

  it('should ...', inject([UnitsSelectionService], (service: UnitsSelectionService) => {
    expect(service).toBeTruthy();
  }));
});

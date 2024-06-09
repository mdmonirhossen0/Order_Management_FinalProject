import { TestBed } from '@angular/core/testing';

import { ProcurementDetailsService } from './procurement-details.service';

describe('ProcurementDetailsService', () => {
  let service: ProcurementDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcurementDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

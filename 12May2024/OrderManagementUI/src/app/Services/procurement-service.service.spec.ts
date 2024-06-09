import { TestBed } from '@angular/core/testing';

import { ProcurementServiceService } from './procurement-service.service';

describe('ProcurementServiceService', () => {
  let service: ProcurementServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcurementServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

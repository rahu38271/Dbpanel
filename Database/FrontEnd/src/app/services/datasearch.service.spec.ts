import { TestBed } from '@angular/core/testing';

import { DatasearchService } from './datasearch.service';

describe('DatasearchService', () => {
  let service: DatasearchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DatasearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

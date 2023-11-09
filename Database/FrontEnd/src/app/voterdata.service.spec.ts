import { TestBed } from '@angular/core/testing';

import { VoterdataService } from './voterdata.service';

describe('VoterdataService', () => {
  let service: VoterdataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VoterdataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

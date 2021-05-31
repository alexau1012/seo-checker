import { TestBed } from '@angular/core/testing';

import { CheckSeoService } from './check-seo.service';

describe('CheckSeoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CheckSeoService = TestBed.get(CheckSeoService);
    expect(service).toBeTruthy();
  });
});

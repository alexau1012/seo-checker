import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { CheckSeoService } from './shared/check-seo.service';

import { CheckSeoResult } from './shared/check-seo-result.model';

import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './check-seo.component.html',
})
export class CheckSeoComponent {

  companyName: string = 'www.sympli.com.au';

  searchEngineOptions: string[] = environment.supportedSearchEngines;
  seoCheckerForm: FormGroup;

  errorMessage: string = '';

  result: CheckSeoResult;

  constructor(private checkSeoService: CheckSeoService) { }

  ngOnInit(): void {
    this.seoCheckerForm = new FormGroup({
      searchEngine: new FormControl(this.searchEngineOptions[0], Validators.required),
      keywords: new FormControl('e-settlements', Validators.required)
    })
  }

  get searchEngine() {
    return this.seoCheckerForm.get('searchEngine');
  }

  get keywords() {
    return this.seoCheckerForm.get('keywords');
  }

  onSubmit() {
    // Clear previous error message
    this.errorMessage = '';

    this.checkSeoService.getRanks(this.searchEngine.value, this.keywords.value).subscribe(
      (result: CheckSeoResult) => {
        if (result === null) {
          this.errorMessage = 'Unable to check SEO performance. Please try again later or use a different setting.';
        }
        console.log(result);
        this.result = result;
      }
    );
  }
}

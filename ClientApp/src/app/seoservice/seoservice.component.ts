import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Search } from './search';
import { CustomValidator } from '../shared/custom.validator';
import { debounceTime } from 'rxjs/operators';
import { SearchService } from './search.service';

@Component({
  selector: 'app-seo-info',
  templateUrl: './seoservice.component.html'
})

export class SeoServieComponent implements OnInit {

  searchForm: FormGroup;
  public searchresult: Search; 
  errorMessage: string;
  SearchEngine: any = ['Google', 'Bing'];

  private validationMessages = {
    required: 'Keyword is required field',
    isInvalidvalue: 'Please enter a valid value.'
  };

  constructor(private fb: FormBuilder, private searchService : SearchService) {
    
  }

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      keyword: ['', [Validators.required, CustomValidator.stringOrURL()]],
      searchResult: '',
      searchEngine: ['']
    });

    const keywordcontrol = this.searchForm.get('keyword');

    keywordcontrol.valueChanges.pipe(
      debounceTime(1000)
    ).subscribe(
      value => this.setMessage(keywordcontrol)
    );
  }

  changeValue(e) {
    this.searchForm.get('searchEngine').setValue(e.target.value, {
         onlySelf: true
    })
    console.log(JSON.stringify(this.searchForm.get('searchEngine').value));
  }

  setMessage(c: AbstractControl): void {
    this.errorMessage = '';
    if (c.errors) {
      this.errorMessage = Object.keys(c.errors).map(
        key => this.validationMessages[key]).join(' ');
    }
  }  

  get searchEngine() {
    return this.searchForm.get('searchEngine').value;
  }

  get keyWord() {
    return this.searchForm.get('keyword').value;
  }

  public getData(): void {
    if (this.searchForm.valid) {
      
      this.searchService.getSearchResult(this.keyWord, this.searchEngine).subscribe(
        {
          next: data => {
            this.errorMessage = '';
            this.searchresult = data;
            this.searchForm.patchValue({
              searchResult: this.searchresult.searchResult
            });
          },
          error: err => { this.errorMessage = 'Search service is not running. Please start service.';} 
        }
      )
    }
    else {
      this.searchForm.patchValue({
        searchResult: ''
      });
      console.log(this.searchForm.value.keyword);
    }    
  }
}



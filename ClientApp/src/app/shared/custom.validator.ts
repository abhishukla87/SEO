import { AbstractControl, ValidatorFn } from '@angular/forms';

export class CustomValidator {

  static stringOrURL(): ValidatorFn {
    return (c: AbstractControl): { [key: string]: boolean } | null => {

      const urlRegex = /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/;
      const numberRegex = /^[0-9]*$/;
      if (c.value && isNaN(c.value) && ( (!numberRegex.test(c.value)) || (urlRegex.test(c.value))))
      {
        return null;        
      }
      return { isInvalidvalue: true };
    };
  }
}

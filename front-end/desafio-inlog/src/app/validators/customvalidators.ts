import { AbstractControl, ValidationErrors } from '@angular/forms';

export function latitudeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;

  if (value === null || value === undefined) return null;

  return value >= -90 && value <= 90 ? null : { invalidLatitude: true };
}

export function longitudeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;

  if (value === null || value === undefined) return null;

  return value >= -180 && value <= 180 ? null : { invalidLongitude: true };
}


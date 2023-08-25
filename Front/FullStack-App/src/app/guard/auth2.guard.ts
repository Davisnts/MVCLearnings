import { CanActivateFn } from '@angular/router';

export const auth2Guard: CanActivateFn = (route, state) => {
  return true;
};

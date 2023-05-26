import { ApiPasswordsData } from '../api';
import { Password } from '../types';

const formPatternFromApiPasswordsData = (apiPass: ApiPasswordsData): string => {
  let pattern = '';

  if (apiPass.uppercase) pattern += 'abc';
  if (apiPass.lowercase) pattern += 'ABC';
  if (apiPass.numbers) pattern += '123';
  if (apiPass.symbols) pattern += '!@#';
  if (apiPass.digits) pattern += '';

  return pattern;
};

export const apiPasswordToPassword = (apiPass: ApiPasswordsData): Password => {
  return {
    id: apiPass.id,
    site: apiPass.site,
    login: apiPass.login,
    pattern: formPatternFromApiPasswordsData(apiPass),
    counter: apiPass.counter,
    length: apiPass.length
  };
};

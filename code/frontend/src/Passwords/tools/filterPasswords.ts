import { Password } from '../types';

import { FilterSettings } from '~/Common';

export function filterPasswords(passwords: Password[], filterSettings: FilterSettings) {
  return passwords.filter(pass => {
    const filterKeys = Object.keys(filterSettings) as Array<'pattern' | 'length'>;

    for (const k of filterKeys) {
      switch (k) {
        case 'length':
          if (filterSettings['length'].length > 0 && !filterSettings['length'].includes(pass.length?.toString() ?? ''))
            return false;
          break;
        case 'pattern':
          if (
            filterSettings['pattern'].length > 0 &&
            filterSettings['pattern'].filter(pattern => pass.pattern?.includes(pattern)).length === 0
          )
            return false;
          break;
      }
    }

    return true;
  });
}

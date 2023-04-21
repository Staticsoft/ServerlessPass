import { Column, Option } from '~/Common';
import { patternFilterOptions } from '~/Passwords/data';
import { Password } from '~/Passwords/types';

export const getColumns = (passwords: Password[]): Column<Password>[] => {
  const uniqueLengths: number[] = passwords
    .reduce((result, { length = -1 }) => {
      return length > -1 && result.includes(length) ? result : [...result, length];
    }, [] as number[])
    .sort((a, b) => a - b);

  const lengthOptions = uniqueLengths.map(
    (length): Option => ({
      key: length.toString(),
      label: length.toString()
    })
  );

  return [
    { name: 'site', title: 'Site', getRowValue: row => row.site ?? '' },
    { name: 'login', title: 'Login', getRowValue: row => row.login ?? '' },
    { name: 'pattern', title: 'Pattern', getRowValue: row => row.pattern ?? '', filterOptions: patternFilterOptions },
    { name: 'length', title: 'Length', getRowValue: row => row.length?.toString() ?? '', filterOptions: lengthOptions },
    { name: 'counter', title: 'Counter', getRowValue: row => row.counter?.toString() ?? '' }
  ];
};

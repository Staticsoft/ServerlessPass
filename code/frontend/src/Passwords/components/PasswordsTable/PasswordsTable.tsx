import { FC, useMemo, useState } from 'react';

import { DataTable, FilterSettings } from '~/Common';

import { PasswordsTableProps } from './PasswordsTable.types';
import { getColumns } from './utils';

export const PasswordsTable: FC<PasswordsTableProps> = props => {
  const { passwords = [] } = props;
  const [filterSettings, setFilterSettings] = useState<FilterSettings>({});

  const columns = useMemo(() => getColumns(passwords), [passwords]);

  const filteredPasswords = () => {
    return passwords.filter(pass => {
      const filterKeys = Object.keys(filterSettings) as Array<'pattern' | 'length'>;

      for (const k of filterKeys) {
        console.log(k);
        switch (k) {
          case 'length':
            console.log(filterSettings['length'], pass.length);
            if (
              filterSettings['length'].length > 0 &&
              !filterSettings['length'].includes(pass.length?.toString() ?? '')
            )
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
  };

  return (
    <DataTable
      columns={columns}
      data={filteredPasswords()}
      getRowKey={row => row.id}
      onFilterChange={setFilterSettings}
    />
  );
};

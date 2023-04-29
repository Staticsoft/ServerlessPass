import { FC, useMemo, useState } from 'react';

import { DataTable, FilterSettings } from '~/Common';
import { useLocale } from '~/locale';
import { filterPasswords } from '~/Passwords/tools';

import { PasswordsTableProps } from './PasswordsTable.types';
import { getColumns } from './utils';

export const PasswordsTable: FC<PasswordsTableProps> = props => {
  const { passwords = [] } = props;
  const [filterSettings, setFilterSettings] = useState<FilterSettings>({});

  const columns = useMemo(() => getColumns(passwords), [passwords]);

  const locale = useLocale();

  const filteredPasswords = () => {
    return filterPasswords(passwords, filterSettings);
  };

  return (
    <DataTable
      columns={columns}
      data={filteredPasswords()}
      getRowKey={row => row.id}
      emptyMessage={locale.passwords.emptyDataMessage}
      onFilterChange={setFilterSettings}
    />
  );
};

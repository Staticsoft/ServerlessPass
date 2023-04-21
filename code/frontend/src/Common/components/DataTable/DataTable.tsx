import { Table, Thead, Tbody, Tr } from '@chakra-ui/react';
import { useEffect, useState } from 'react';

import { HeaderCell, TableCell } from './components';
import { DataTableProps, FilterSettings } from './DataTable.types';

export function DataTable<Data extends object>(props: DataTableProps<Data>) {
  const { data, columns, getRowKey, onFilterChange } = props;

  const [filterSettings, setFilterSettings] = useState<FilterSettings>({});

  useEffect(() => {
    if (onFilterChange) onFilterChange(filterSettings);
  }, [filterSettings]);

  return (
    <Table>
      <Thead>
        <Tr>
          {columns.map(column => (
            <HeaderCell
              key={column.name}
              column={column}
              onFilterChange={selectedKeys =>
                setFilterSettings(prevState => ({ ...prevState, [column.name]: selectedKeys }))
              }
            />
          ))}
        </Tr>
      </Thead>

      <Tbody>
        {data.map(row => {
          const rowKey = getRowKey(row);

          return (
            <Tr key={rowKey}>
              {columns.map(({ name, getRowValue }) => (
                <TableCell key={`${rowKey}-${name}`} value={getRowValue(row)} />
              ))}
            </Tr>
          );
        })}
      </Tbody>
    </Table>
  );
}

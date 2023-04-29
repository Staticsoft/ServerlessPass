import { Table, Thead, Tbody, Tr, Center, Text } from '@chakra-ui/react';
import { useEffect, useState } from 'react';

import { HeaderCell, TableCell } from './components';
import { DataTableProps, FilterSettings } from './DataTable.types';

export function DataTable<Data extends object>(props: DataTableProps<Data>) {
  const { data, columns, emptyMessage, getRowKey, onFilterChange } = props;

  const [filterSettings, setFilterSettings] = useState<FilterSettings>({});

  useEffect(() => {
    if (onFilterChange) onFilterChange(filterSettings);
  }, [filterSettings]);

  return (
    <div>
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
          {data.length > 0 &&
            data.map(row => {
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

      {data.length === 0 && (
        <Center width={'100%'} padding={8}>
          <Text fontSize={25} fontWeight={'semibold'}>
            {emptyMessage}
          </Text>
        </Center>
      )}
    </div>
  );
}

import { Popover, Th, Flex, PopoverTrigger, IconButton, Portal, PopoverContent, PopoverBody } from '@chakra-ui/react';
import { FilterList } from '@mui/icons-material';
import { useEffect } from 'react';

import { CheckboxGroup } from '../../CheckboxGroup';
import { Column } from '../DataTable.types';

import { useCheckboxGroupKeys } from '~/Common/hooks';

interface HeaderCellProps<Data extends object> {
  column: Column<Data>;
  onFilterChange: (selectedKeys: string[]) => void;
}

export function HeaderCell<Data extends object>({ column, onFilterChange }: HeaderCellProps<Data>) {
  const { title, filterOptions = [] } = column;
  const { selectedKeys, onChange, onToggleAll } = useCheckboxGroupKeys(filterOptions);

  useEffect(() => {
    if (filterOptions.length > 0) onFilterChange(selectedKeys);
  }, [filterOptions, selectedKeys]);

  return (
    <Popover>
      <Th>
        <Flex justifyContent={'space-between'} alignItems={'center'}>
          {title}

          {filterOptions.length > 0 && (
            <PopoverTrigger>
              <IconButton colorScheme="blue" aria-label="Search database" icon={<FilterList />} />
            </PopoverTrigger>
          )}
        </Flex>

        <Portal>
          <PopoverContent minWidth={'3xs'} width={'auto'}>
            <PopoverBody padding={'12px 16px'}>
              <CheckboxGroup
                options={filterOptions}
                selectedKeys={selectedKeys}
                onChange={onChange}
                onToggleAll={onToggleAll}
              />
            </PopoverBody>
          </PopoverContent>
        </Portal>
      </Th>
    </Popover>
  );
}

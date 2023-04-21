import { Checkbox, Stack } from '@chakra-ui/react';
import { FC } from 'react';

import { useLocale } from '~/locale';

import { CheckboxGroupProps } from './CheckboxGroup.types';

export const CheckboxGroup: FC<CheckboxGroupProps> = props => {
  const { options, selectedKeys, onChange, onToggleAll } = props;
  const locale = useLocale();

  return (
    <Stack spacing={[1, 5]}>
      <Checkbox
        isChecked={selectedKeys.length === options.length}
        isIndeterminate={selectedKeys.length > 0 && selectedKeys.length !== options.length}
        onChange={onToggleAll}
      >
        {locale.checkboxes.selectAll}
      </Checkbox>

      {options.map(({ key, label }) => (
        <Checkbox key={key} isChecked={selectedKeys.indexOf(key) > -1} onChange={() => onChange(key)}>
          {label}
        </Checkbox>
      ))}
    </Stack>
  );
};

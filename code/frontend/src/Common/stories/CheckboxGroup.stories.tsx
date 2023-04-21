import { Meta, StoryFn } from '@storybook/react';

import { CheckboxGroup } from '../components';
import { useCheckboxGroupKeys } from '../hooks';

import { patternFilterOptions } from '~/Passwords/data';

export default {
  title: 'Common/components/CheckboxGroup'
} as Meta;

export const Default: StoryFn = () => {
  const { selectedKeys, onChange, onToggleAll } = useCheckboxGroupKeys(patternFilterOptions);

  return (
    <CheckboxGroup
      options={patternFilterOptions}
      selectedKeys={selectedKeys}
      onChange={onChange}
      onToggleAll={onToggleAll}
    />
  );
};

Default.args = {};
Default.storyName = 'CheckboxGroup';

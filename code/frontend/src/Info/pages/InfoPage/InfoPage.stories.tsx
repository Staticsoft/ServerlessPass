import { Meta, Story } from '@storybook/react';

import { InfoPage } from './InfoPage';

export default {
  title: 'InfoPage'
} as Meta;

interface Args {}

export const Default: Story<Args> = args => {
  return <InfoPage {...args} />;
};

Default.args = {};

Default.storyName = 'InfoPage';

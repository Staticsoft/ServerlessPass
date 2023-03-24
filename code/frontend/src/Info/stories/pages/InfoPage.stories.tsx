import { Meta, Story } from '@storybook/react';

import { InfoPage } from '~/Info';

export default {
  title: 'InfoPage'
} as Meta;

export const Default: Story = args => {
  return <InfoPage {...args} />;
};

Default.args = {};

Default.storyName = 'InfoPage';

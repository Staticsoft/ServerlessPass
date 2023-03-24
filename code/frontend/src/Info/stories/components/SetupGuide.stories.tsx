import { Meta, Story } from '@storybook/react';

import { SetupGuide } from '~/Info/components';

export default {
  title: 'Info/SetupGuide',
  parameters: {
    layout: 'centered'
  }
} as Meta;

export const Default: Story = args => {
  return <SetupGuide {...args} />;
};

Default.args = {};

Default.storyName = 'SetupGuide';

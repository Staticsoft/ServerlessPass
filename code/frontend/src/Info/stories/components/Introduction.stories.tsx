import { Meta, Story } from '@storybook/react';

import { Introduction } from '~/Info';

export default {
  title: 'Info/Introduction',
  parameters: {
    layout: 'centered'
  }
} as Meta;

export const Default: Story = args => {
  return <Introduction {...args} />;
};

Default.storyName = 'Introduction';

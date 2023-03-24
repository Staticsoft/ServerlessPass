import { Meta, Story } from '@storybook/react';

import { Footer } from '~/Info';

export default {
  title: 'Info/Footer',
  parameters: {
    layout: 'centered'
  }
} as Meta;

export const Default: Story = args => {
  return <Footer {...args} />;
};

Default.storyName = 'Footer';

import { Meta, StoryFn } from '@storybook/react';

import { Footer } from '~/Info';

export default {
  title: 'Info/components/Footer',
  parameters: {
    layout: 'centered'
  }
} as Meta;

export const Default: StoryFn = () => {
  return <Footer />;
};

Default.storyName = 'Footer';

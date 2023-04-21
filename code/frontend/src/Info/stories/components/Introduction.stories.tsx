import { Meta, StoryFn } from '@storybook/react';

import { Introduction } from '~/Info';

export default {
  title: 'Info/Introduction',
  parameters: {
    layout: 'centered'
  }
} as Meta;

export const Default: StoryFn = () => {
  return <Introduction />;
};

Default.storyName = 'Introduction';

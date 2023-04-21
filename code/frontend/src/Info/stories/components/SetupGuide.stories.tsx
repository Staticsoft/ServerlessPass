import { Meta, StoryFn } from '@storybook/react';

import { SetupGuide } from '~/Info/components';

export default {
  title: 'Info/SetupGuide',
  parameters: {
    layout: 'centered'
  }
} as Meta;

export const Default: StoryFn = () => {
  return <SetupGuide />;
};

Default.storyName = 'SetupGuide';

import { Meta, StoryFn } from '@storybook/react';

import { ServerlessPassTitle } from '~/Info';

export default {
  title: 'Info/components/ServerlessPassTitle',
  parameters: {
    layout: 'centered'
  }
} as Meta;

export const Default: StoryFn = () => {
  return <ServerlessPassTitle />;
};

Default.storyName = 'ServerlessPassTitle';

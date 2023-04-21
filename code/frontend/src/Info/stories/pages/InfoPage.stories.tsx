import { Meta, StoryFn } from '@storybook/react';

import { InfoPage } from '~/Info';

export default {
  title: 'Info/pages/InfoPage'
} as Meta;

export const Default: StoryFn = () => {
  return <InfoPage />;
};

Default.storyName = 'InfoPage';

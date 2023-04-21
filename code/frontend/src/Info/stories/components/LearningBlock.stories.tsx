import { PhoneIphone } from '@mui/icons-material';
import type { Meta, StoryFn } from '@storybook/react';

import { LearningBlock, LearningBlockProps } from '~/Info';

export default {
  title: 'Info/components/LearningBlock'
} as Meta;

type Args = LearningBlockProps;

export const Default: StoryFn<Args> = args => {
  return <LearningBlock {...args} />;
};

Default.args = {
  icon: PhoneIphone,
  title: 'Configure LessPass',
  text: 'Configure LessPass to use https://api.lesspass.staticsoft.org \n and sign in using your new account'
};
Default.storyName = 'LearningBlock';

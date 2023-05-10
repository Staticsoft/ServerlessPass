import { Meta, StoryFn } from '@storybook/react';

import { usePasswords } from '../../hooks';
import { PasswordPage } from '../../pages';

export default {
  title: 'Passwords/pages/PasswordPage'
} as Meta;

export const Default: StoryFn = () => {
  return <PasswordPage usePasswords={usePasswords} />;
};

Default.storyName = 'PasswordPage';

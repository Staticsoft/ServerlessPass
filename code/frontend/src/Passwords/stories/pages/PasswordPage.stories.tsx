import { Meta, StoryFn } from '@storybook/react';

import { PasswordPage } from '../../pages';

import { getFakePasswords } from '~/Passwords/mocks';

export default {
  title: 'Passwords/pages/PasswordPage'
} as Meta;

export const Default: StoryFn = () => {
  return <PasswordPage getPasswords={getFakePasswords} />;
};

Default.storyName = 'PasswordPage';

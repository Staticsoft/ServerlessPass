import { Meta, StoryFn } from '@storybook/react';

import { PasswordsTable } from '../../components';

import { createPassword } from '~/Passwords/mocks';

export default {
  title: 'Passwords/components/PasswordsTable'
} as Meta;

export const Default: StoryFn = () => {
  return <PasswordsTable passwords={[createPassword(0), createPassword(1), createPassword(2)]} />;
};

Default.storyName = 'PasswordsTable';

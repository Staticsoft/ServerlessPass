import { Meta, StoryFn } from '@storybook/react';

import { PasswordsTable } from '../components/PasswordsTable';
import { patterns } from '../data';
import { Password } from '../types';

export default {
  title: 'Passwords/PasswordsTable'
} as Meta;

function generatePattern(): string {
  const first = Math.floor(Math.random() * 4);
  let second = 0;

  do {
    second = Math.floor(Math.random() * 4);
  } while (first === second);

  return [patterns[first], patterns[second]].join('');
}

function createPassword(number: number): Password {
  return {
    id: `pass-${number}`,
    site: `site-${number}.com`,
    login: `User ${number}`,
    pattern: generatePattern(),
    length: Math.floor(Math.random() * 12) + 4,
    counter: 1
  };
}

export const Default: StoryFn = () => {
  return <PasswordsTable passwords={[createPassword(0), createPassword(1), createPassword(2)]} />;
};

Default.storyName = 'PasswordsTable';

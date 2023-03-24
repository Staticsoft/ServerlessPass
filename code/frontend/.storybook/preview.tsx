import { ChakraProvider } from '@chakra-ui/react';
import type { Preview } from '@storybook/react';
import React from 'react';

const preview: Preview = {
  parameters: {
    backgrounds: {
      default: 'light'
    },
    actions: { argTypesRegex: '^on[A-Z].*' },
    controls: {
      matchers: {
        color: /(background|color)$/i,
        date: /Date$/
      }
    }
  },
  decorators: [
    Story => (
      <ChakraProvider>
        <Story />
      </ChakraProvider>
    )
  ]
};

export default preview;

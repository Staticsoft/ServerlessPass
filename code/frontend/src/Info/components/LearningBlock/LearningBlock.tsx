import { Stack, Text } from '@chakra-ui/react';
import React from 'react';

import { LearningBlockProps } from './LearningBlock.types';

export const LearningBlock: React.FC<LearningBlockProps> = props => {
  const { icon: Icon, text, title } = props;

  return (
    <Stack maxWidth={300} spacing={'8px'}>
      <Icon />

      <Text fontSize={'lg'} fontWeight={'bold'}>
        {title}
      </Text>

      <Text fontSize={'lg'}>{text}</Text>
    </Stack>
  );
};

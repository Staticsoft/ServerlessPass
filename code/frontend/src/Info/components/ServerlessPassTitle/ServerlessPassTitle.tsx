import { Flex, Text } from '@chakra-ui/react';

import { useLocale } from '~/locale';

export const ServerlessPassTitle: React.FC = () => {
  const { root } = useLocale();

  return (
    <Flex alignItems={'flex-start'}>
      <Text fontSize={56} fontWeight={'bold'}>
        {root.header.title}
      </Text>

      <Text fontSize={'sm'} fontWeight={'bold'}>
        {root.header.tm}
      </Text>
    </Flex>
  );
};

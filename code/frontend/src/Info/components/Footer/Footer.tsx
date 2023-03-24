import { Button, Stack, Text } from '@chakra-ui/react';

import { useLocale } from '~/locale';

export const Footer: React.FC = () => {
  const { root, buttons } = useLocale();

  return (
    <Stack spacing={'24px'} alignItems={'center'}>
      <Text fontSize={35} fontWeight={'bold'}>
        {root.footer.title}
      </Text>

      <Text fontSize={21}>{root.footer.text}</Text>

      <Button colorScheme={'messenger'} width={150}>
        {buttons.gitHub}
      </Button>
    </Stack>
  );
};

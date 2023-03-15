import { Button, Flex, HStack, Image, Stack, Text } from '@chakra-ui/react';

import Logo from '~/assets/logo.png';
import { useLocale } from '~/locale';

import { IntroductionProps } from './Introduction.types';

export const Introduction: React.FC<IntroductionProps> = () => {
  return (
    <HStack spacing={'80px'}>
      <Stack maxWidth={{ sm: '100%', md: 550 }} spacing={'24px'}>
        <Title />

        <Description />

        <Links />
      </Stack>

      <Image display={{ sm: 'none', md: 'unset' }} src={Logo} />
    </HStack>
  );
};

const Title: React.FC = () => {
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

const Description: React.FC = () => {
  const { root } = useLocale();

  return (
    <>
      {root.header.description.map((paragrath, index) => {
        return (
          <Text key={`paragraph-${index}`} fontSize={24}>
            {paragrath}
          </Text>
        );
      })}
    </>
  );
};

const Links: React.FC = () => {
  const { buttons } = useLocale();

  return (
    <HStack width={'100%'} spacing={'40px'} justifyContent={'center'}>
      <Button width={131} colorScheme={'messenger'} variant={'solid'}>
        {buttons.signUp}
      </Button>

      <Button width={131} colorScheme={'messenger'} variant={'outline'}>
        {buttons.hostAWS}
      </Button>
    </HStack>
  );
};

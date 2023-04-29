import { HStack, Image, Stack } from '@chakra-ui/react';
import { FC } from 'react';

import { ServerlessPassTitle } from '../ServerlessPassTitle';

import Logo from '~/assets/logo.png';

import { Links, Description } from './components';
import { IntroductionProps } from './Introduction.types';

export const Introduction: FC<IntroductionProps> = props => {
  const { onSignUp, onHostAWS } = props;

  return (
    <HStack spacing={'80px'}>
      <Stack maxWidth={{ sm: '100%', md: 550 }} spacing={'24px'}>
        <ServerlessPassTitle />

        <Description />

        <Links onSignUp={onSignUp} onHostAWS={onHostAWS} />
      </Stack>

      <Image display={{ sm: 'none', md: 'unset' }} src={Logo} />
    </HStack>
  );
};

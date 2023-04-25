import { HStack, Button } from '@chakra-ui/react';
import { FC } from 'react';

import { ActionsProps } from '../Introduction.types';

import { useLocale } from '~/locale';

export const Links: FC<ActionsProps> = ({ onSignUp, onHostAWS }) => {
  const { buttons } = useLocale();

  return (
    <HStack width={'100%'} spacing={'40px'} justifyContent={'center'}>
      <Button width={131} colorScheme={'messenger'} variant={'solid'} onClick={onSignUp}>
        {buttons.signUp}
      </Button>

      <Button width={131} colorScheme={'messenger'} variant={'outline'} onClick={onHostAWS}>
        {buttons.hostAWS}
      </Button>
    </HStack>
  );
};

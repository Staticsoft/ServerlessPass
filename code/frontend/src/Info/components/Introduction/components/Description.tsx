import { Text } from '@chakra-ui/react';
import { FC } from 'react';

import { useLocale } from '~/locale';

export const Description: FC = () => {
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

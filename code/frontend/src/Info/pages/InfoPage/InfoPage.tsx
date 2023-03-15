import { Stack } from '@chakra-ui/react';

import { Introduction, Footer, SetupGuide } from '~/Info/components';

import classes from './InfoPage.styles.module.scss';
import { InfoPageProps } from './InfoPage.types';

export const InfoPage: React.FC<InfoPageProps> = () => {
  return (
    <div className={classes.infopage}>
      <Stack spacing={'80px'} alignItems={'center'}>
        <Introduction />

        <SetupGuide />

        <Footer />
      </Stack>
    </div>
  );
};

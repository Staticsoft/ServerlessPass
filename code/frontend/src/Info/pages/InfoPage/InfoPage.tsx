import { Stack } from '@chakra-ui/react';

import { Introduction, Footer, SetupGuide } from '~/Info/components';

import classes from './InfoPage.styles.module.scss';

export const InfoPage: React.FC = () => {
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

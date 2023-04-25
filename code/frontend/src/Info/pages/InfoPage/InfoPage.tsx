import { Stack } from '@chakra-ui/react';

import { useAuth } from '~/Auth';
import { Introduction, Footer, SetupGuide } from '~/Info/components';

import classes from './InfoPage.styles.module.scss';

export const InfoPage: React.FC = () => {
  const { signIn: signIn } = useAuth();

  return (
    <div className={classes.infopage}>
      <Stack spacing={'80px'} alignItems={'center'}>
        <Introduction onSignUp={signIn} />

        <SetupGuide />

        <Footer />
      </Stack>
    </div>
  );
};

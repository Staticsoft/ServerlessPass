import { Stack } from '@chakra-ui/react';
import { FC, useMemo } from 'react';

import { ServerlessPassTitle } from '~/Info/components';
import { PasswordsTable } from '~/Passwords/components';
import { Password } from '~/Passwords/types';

import classes from './PasswordPage.styles.module.scss';

interface Props {
  getPasswords: () => Password[];
}

export const PasswordPage: FC<Props> = ({ getPasswords }) => {
  const passwords = useMemo(() => {
    return getPasswords();
  }, [getPasswords]);

  return (
    <div className={classes.page}>
      <Stack spacing={160}>
        <ServerlessPassTitle />

        <PasswordsTable passwords={passwords} />
      </Stack>
    </div>
  );
};

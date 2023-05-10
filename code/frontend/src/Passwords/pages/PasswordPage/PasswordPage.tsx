import { HStack, Stack } from '@chakra-ui/react';
import { FC } from 'react';

import { ServerlessPassTitle } from '~/Info/components';
import { PasswordsApi } from '~/Passwords/api';
import { ImportButton, PasswordsTable } from '~/Passwords/components';
import { UsePasswordsHook } from '~/Passwords/hooks';

import classes from './PasswordPage.styles.module.scss';

interface Props {
  passwordsApi: PasswordsApi;
  usePasswords: UsePasswordsHook;
}

export const PasswordPage: FC<Props> = props => {
  const { passwordsApi, usePasswords } = props;

  const { passwords, importPasswords } = usePasswords(passwordsApi);

  return (
    <div className={classes.page}>
      <Stack spacing={160}>
        <ServerlessPassTitle />

        <Stack spacing={10}>
          <HStack width={'100%'} justifyContent={'flex-end'}>
            <ImportButton onImport={importPasswords} />
          </HStack>

          <PasswordsTable passwords={passwords} />
        </Stack>
      </Stack>
    </div>
  );
};

import { Stack } from '@chakra-ui/react';
import { FC, useEffect, useState } from 'react';

import { useAuth } from '~/Auth';
import { ServerlessPassTitle } from '~/Info/components';
import { ApiPasswordsData, passwordsApi } from '~/Passwords/api';
import { PasswordsTable } from '~/Passwords/components';
import { Password } from '~/Passwords/types';

import classes from './PasswordPage.styles.module.scss';

interface Props {
  getPasswords: () => Password[];
}

const formPatterFromApiPasswordsData = (apiPass: ApiPasswordsData): string => {
  let pattern = '';

  if (apiPass.uppercase) pattern += 'abc';
  if (apiPass.lowercase) pattern += 'ABC';
  if (apiPass.numbers) pattern += '123';
  if (apiPass.symbols) pattern += '!@#';
  if (apiPass.digits) pattern += '';

  return pattern;
};

const apiPasswordToPassword = (apiPass: ApiPasswordsData): Password => {
  return {
    id: apiPass.id,
    site: apiPass.site,
    login: apiPass.login,
    pattern: formPatterFromApiPasswordsData(apiPass),
    counter: apiPass.counter,
    length: apiPass.length
  };
};

export const PasswordPage: FC<Props> = () => {
  const [passwords, setPasswords] = useState<Password[]>([]);
  const { token } = useAuth();

  useEffect(() => {
    (async () => {
      const apiPasswords = await passwordsApi.getPasswordsList(token);

      setPasswords(apiPasswords.map(apiPasswordToPassword));
    })();
  });

  return (
    <div className={classes.page}>
      <Stack spacing={160}>
        <ServerlessPassTitle />

        <PasswordsTable passwords={passwords} />
      </Stack>
    </div>
  );
};

import { useMemo } from 'react';

import { Authenticator } from '~/Auth';
import { PasswordsApi } from '~/Passwords';

import { useConfig } from './useConfig';

export const useDependencies = () => {
  const { config, loadingConfig } = useConfig();

  const { devAuthenticator, passwordsApi } = useMemo(() => {
    return {
      devAuthenticator: new Authenticator(config.auth),
      passwordsApi: new PasswordsApi(config.backend)
    };
  }, [config]);

  return { devAuthenticator, passwordsApi, loadingConfig };
};

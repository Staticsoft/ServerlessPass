import { useMemo } from 'react';

import { Authenticator } from '~/Auth';
import { AuthApi } from '~/Auth/AuthApi';
import { PasswordsApi } from '~/Passwords';

import { useConfig } from './useConfig';

export const useDependencies = () => {
  const { config, loadingConfig } = useConfig();

  const { devAuthenticator, passwordsApi } = useMemo(() => {
    const authApi = new AuthApi(config.clientId, config.redirectUri, config.auth);
    return {
      devAuthenticator: new Authenticator(config.auth, config.redirectUri, config.clientId, authApi),
      passwordsApi: new PasswordsApi(config.backend)
    };
  }, [config]);

  return { devAuthenticator, passwordsApi, loadingConfig };
};

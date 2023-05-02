import { getTokenFromStorage, getTokenFromUrl, putTokenToStorage, removeTokenFromStorage } from '../Token';

import { Authenticator } from './Authenticator.types';

function redirectToAuthPage(authUrl: string) {
  window.location.replace(`${authUrl}?redirect_uri=${window.location.href}&response_type=code&scope=openid`);
}

export class DevAuthenticator implements Authenticator {
  getToken = () => {
    let token = getTokenFromStorage();

    if (!token) {
      token = getTokenFromUrl();
      if (token) putTokenToStorage(token);
    }

    return token;
  };

  autoSignIn = async () => {
    const token = getTokenFromStorage() ?? getTokenFromUrl();

    return token;
  };

  signIn = async () => {
    const token = this.getToken();

    const config = await (await fetch('/config.json')).json();
    if (!token) redirectToAuthPage(`${config.auth}/login`);

    return token;
  };

  signOut = async () => removeTokenFromStorage();
}

export const devAuthenticator = new DevAuthenticator();

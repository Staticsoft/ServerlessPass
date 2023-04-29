import { getTokenFromStorage, getTokenFromUrl, putTokenToStorage, removeTokenFromStorage } from '../Token';

import { Authenticator } from './Authenticator.types';

const authUrl = 'http://localhost:5002/login';

function redirectToAuthPage() {
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

    if (!token) redirectToAuthPage();

    return token;
  };

  signOut = async () => removeTokenFromStorage();
}

export const devAuthenticator = new DevAuthenticator();

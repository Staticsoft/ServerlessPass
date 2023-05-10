import {
  getTokenFromStorage,
  getTokenFromUrl,
  putTokenToStorage,
  removeTokenFromStorage,
  redirectToAuthPage
} from './tools';

export class Authenticator {
  constructor(private authUrl: string) {}

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

    if (!token) redirectToAuthPage(this.authUrl);

    return token;
  };

  signOut = async () => removeTokenFromStorage();
}

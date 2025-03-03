import { AuthApi } from './AuthApi';
import {
  getTokenFromStorage,
  getCodeFromUrl,
  putTokenToStorage,
  removeTokenFromStorage,
  redirectToAuthPage
} from './tools';

export class Authenticator {
  constructor(private authUrl: string, private redirectUri: string, private clientId: string, private api: AuthApi) {}

  getStoredToken = (): Promise<string | null> => {
    return this.getToken();
  };

  signIn = async (): Promise<string | null> => {
    const token = await this.getToken();

    if (!token) {
      redirectToAuthPage(this.authUrl, this.redirectUri, this.clientId);
      return null;
    }

    return token;
  };

  signOut = (): void => removeTokenFromStorage();

  private getToken = async (): Promise<string | null> => {
    let token = getTokenFromStorage();
    if (token) return token;

    token = await this.getTokenFromCode();
    if (token) putTokenToStorage(token);

    return token;
  };

  private getTokenFromCode = async (): Promise<string | null> => {
    const code = getCodeFromUrl();
    if (!code) return null;

    return this.api.exchangeCode(code);
  };
}

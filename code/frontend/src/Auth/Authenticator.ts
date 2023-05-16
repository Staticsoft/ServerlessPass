import {
  getTokenFromStorage,
  getCodeFromUrl,
  putTokenToStorage,
  removeTokenFromStorage,
  redirectToAuthPage
} from './tools';

export class Authenticator {
  constructor(private authUrl: string, private redirectUri: string, private clientId: string) {}

  getToken = async () => {
    let token = getTokenFromStorage();

    if (!token) {
      const code = getCodeFromUrl();
      token = await this.exchangeCode(code);
      if (token) putTokenToStorage(token);
    }

    return token;
  };

  exchangeCode = async (code: string | null) : Promise<string | null> => {
    if (!code) return null;

    const body = `grant_type=authorization_code&client_id=${this.clientId}&code=${code}&redirect_uri=${this.redirectUri}`;
    const response = await fetch(`${this.authUrl}/oauth2/token`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
      },
      body
    });
    const responseBody = await response.json();
    return responseBody.id_token;
  }

  autoSignIn = async () => {
    const token = getTokenFromStorage() ?? getCodeFromUrl();

    return token;
  };

  signIn = async () => {
    const token = await this.getToken();

    if (!token) redirectToAuthPage(this.authUrl, this.redirectUri);

    return token;
  };

  signOut = async () => removeTokenFromStorage();
}

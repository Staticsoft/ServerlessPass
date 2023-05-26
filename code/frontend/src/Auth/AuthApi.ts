export class AuthApi {
  constructor(private clientId: string, private redirectUri: string, private authUrl: string) {}

  exchangeCode = async (code: string | null): Promise<string | null> => {
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
  };
}

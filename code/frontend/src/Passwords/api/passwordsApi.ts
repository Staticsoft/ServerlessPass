export interface ApiPasswordsData {
  id: string;
  created: string;
  modified: string;
  login: string;
  site: string;
  uppercase: boolean;
  lowercase: boolean;
  numbers: boolean;
  symbols: boolean;
  digits: boolean;
  length: number;
  counter: number;
  version: number;
}

export class PasswordsApi {
  private passwordsUrl: string;

  constructor(backendUrl: string) {
    this.passwordsUrl = backendUrl + '/passwords';
  }

  getPasswordsList = async (token?: string | null): Promise<ApiPasswordsData[]> => {
    if (!token) return [];

    const response = await fetch(this.passwordsUrl, {
      method: 'GET',
      headers: { 'Content-Type': 'application/json', Authorization: `Bearer ${token}` }
    });

    const passwords = await response.json();

    return passwords.results as ApiPasswordsData[];
  };

  importPasswords = async (token?: string | null, passwordsJSON?: string): Promise<void> => {
    if (!token || !passwordsJSON) return;

    await fetch(this.passwordsUrl, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json', Authorization: `Bearer ${token}` },
      body: passwordsJSON
    });
  };
}

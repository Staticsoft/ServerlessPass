import { Authenticator } from './Authenticator.types';

export const fakeAuthenticator: Authenticator = {
  getToken: () => 'token',
  signIn: async () => 'token',
  signOut: async () => false
};

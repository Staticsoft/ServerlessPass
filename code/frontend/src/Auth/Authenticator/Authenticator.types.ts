export interface Authenticator {
  getToken: () => string | null;
  signIn: () => Promise<string | null>;
  signOut: () => void;
}

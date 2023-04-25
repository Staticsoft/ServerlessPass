import { createContext } from 'react';

export interface AuthContextType {
  logedIn?: boolean;
  token?: string | null;
  signIn?: () => void;
  signOut?: () => void;
}

export const AuthContext = createContext<AuthContextType>({});

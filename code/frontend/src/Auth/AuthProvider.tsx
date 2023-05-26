import { ReactNode, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { AuthContext } from './AuthContext';
import { Authenticator } from './Authenticator';

interface AuthProviderProps {
  authenticator: Authenticator;
  children: ReactNode;
}

export function AuthProvider({ authenticator, children }: AuthProviderProps) {
  const [logedIn, setLogedIn] = useState<boolean>(false);
  const [token, setToken] = useState<string | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    (async () => {
      const storedToken = await authenticator.getStoredToken();
      if (!storedToken) return;

      setToken(storedToken);
      setLogedIn(Boolean(storedToken));

      navigate('/passwords');
    })();
  }, []);

  const signIn = async () => {
    const newToken = await authenticator.signIn();

    if (newToken) {
      setToken(newToken);
      setLogedIn(Boolean(newToken));

      navigate('/passwords');
    }
  };

  const signOut = async () => {
    authenticator.signOut();

    setLogedIn(false);
  };

  return <AuthContext.Provider value={{ token, logedIn, signIn, signOut }}>{children}</AuthContext.Provider>;
}

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
      const newToken = await authenticator.getToken();

      setToken(newToken);
      setLogedIn(Boolean(setToken));

      if (newToken) navigate('/passwords');
    })();
  }, []);

  const signIn = async () => {
    const newToken = await authenticator.signIn();

    setToken(newToken);
    setLogedIn(Boolean(setToken));

    if (newToken) navigate('/passwords');
  };

  const signOut = async () => {
    authenticator.signOut();

    setLogedIn(false);
  };

  return <AuthContext.Provider value={{ token, logedIn, signIn, signOut }}>{children}</AuthContext.Provider>;
}

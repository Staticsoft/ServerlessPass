import { ChakraProvider } from '@chakra-ui/react';
import { Route, Routes } from 'react-router-dom';

import { AuthProvider } from './Auth';
import { useDependencies } from './DependeciesInjection/hooks';
import { InfoPage } from './Info';
import { PasswordPage, usePasswords } from './Passwords';

export function App() {
  const { devAuthenticator, passwordsApi, loadingConfig } = useDependencies();

  if (loadingConfig) return <div />;

  return (
    <ChakraProvider>
      <AuthProvider authenticator={devAuthenticator}>
        <Routes>
          <Route path="/" element={<InfoPage />} />

          <Route path="/passwords" element={<PasswordPage passwordsApi={passwordsApi} usePasswords={usePasswords} />} />
        </Routes>
      </AuthProvider>
    </ChakraProvider>
  );
}

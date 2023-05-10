import { ChakraProvider } from '@chakra-ui/react';
import { Route, Routes } from 'react-router-dom';

import { AuthProvider, devAuthenticator } from './Auth';
import { InfoPage } from './Info';
import { PasswordPage } from './Passwords';
import { usePasswords } from './Passwords/hooks';

export function App() {
  return (
    <ChakraProvider>
      <AuthProvider authenticator={devAuthenticator}>
        <Routes>
          <Route path="/" element={<InfoPage />} />

          <Route path="/passwords" element={<PasswordPage usePasswords={usePasswords} />} />
        </Routes>
      </AuthProvider>
    </ChakraProvider>
  );
}

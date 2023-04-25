import { ChakraProvider } from '@chakra-ui/react';
import { Route, Routes } from 'react-router-dom';

import { AuthProvider, devAuthenticator } from './Auth';
import { InfoPage } from './Info';
import { PasswordPage } from './Passwords';
import { getFakePasswords } from './Passwords/mocks';

export function App() {
  return (
    <ChakraProvider>
      <AuthProvider authenticator={devAuthenticator}>
        <Routes>
          <Route path="/" element={<InfoPage />} />

          <Route path="/passwords" element={<PasswordPage getPasswords={getFakePasswords} />} />
        </Routes>
      </AuthProvider>
    </ChakraProvider>
  );
}

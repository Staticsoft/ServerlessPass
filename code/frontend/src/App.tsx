import { ChakraProvider } from '@chakra-ui/react';
import { PhoneIphone } from '@mui/icons-material';

import { LearningBlock } from './Info';

export function App() {
  return (
    <ChakraProvider>
      <LearningBlock icon={PhoneIphone} text="" title="" />
    </ChakraProvider>
  );
}

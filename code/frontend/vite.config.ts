import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import sassDts from 'vite-plugin-sass-dts';

import path from 'path';

export default defineConfig({
  build: {
    outDir: '../../.local/Frontend'
  },
  resolve: {
    alias: {
      '~': path.resolve(__dirname, './src')
    }
  },
  plugins: [react(), sassDts()],
  server: {
    port: 5000
  }
});

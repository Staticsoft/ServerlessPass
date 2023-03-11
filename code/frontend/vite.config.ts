import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import sassDts from 'vite-plugin-sass-dts'

export default defineConfig({
  build: {
    outDir: '../../.local/Frontend'
  },
  plugins: [
    react(),
    sassDts()
  ]
})

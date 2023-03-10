import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import typedScssModules from 'typed-scss-modules';
import sassDts from 'vite-plugin-sass-dts'

// https://vitejs.dev/config/
export default defineConfig({
  // css: {
  //   preprocessorOptions: {
  //     scss: {
  //       preprocessor: typedScssModules,
  //     },
  //   }
  // },
  plugins: [
    react(),
    sassDts()
  ],
})

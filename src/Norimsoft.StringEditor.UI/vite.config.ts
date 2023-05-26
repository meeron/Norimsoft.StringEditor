import { defineConfig } from 'vite';
import solidPlugin from 'vite-plugin-solid';

export default defineConfig({
  base: '/strings',
  resolve: {
    alias: {
      '@': './src',
    },
  },
  plugins: [solidPlugin()],
  server: {
    port: 3000,
    proxy: {
      '/strings/api': 'http://localhost:5079',
    },
  },
  build: {
    target: 'esnext',
    outDir: '../Norimsoft.StringEditor/wwwroot',
  },
});

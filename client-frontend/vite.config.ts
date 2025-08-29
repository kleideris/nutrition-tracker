import { defineConfig } from 'vite'
import tailwindcss from '@tailwindcss/vite'
import react from '@vitejs/plugin-react'
import dotenv from 'dotenv';
import path from 'path';


// Load environment variables from .env file
dotenv.config();

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(), 
    tailwindcss()
  ],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
    },
  },
  server: {
    open: true, // This opens the browser automatically
    port: parseInt(process.env.VITE_PORT ?? '5173'), // This sets the frontend port from the .env
    // proxy: {
    //   '/api': {
    //   target: 'http://localhost:5000',
    //   changeOrigin: true,
    //   secure: false,
    //   }
    // }
  }
})

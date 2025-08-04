import { defineConfig } from 'vite'
import tailwindcss from '@tailwindcss/vite'
import react from '@vitejs/plugin-react'
import dotenv from 'dotenv';


console.log("Using port:", process.env.PORT);

// Load environment variables from .env file
dotenv.config();

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(), 
    tailwindcss()
  ],
  server: {
    open: true, // This opens the browser automatically
    port: parseInt(process.env.PORT ?? '5173'), // This sets the frontend port from the .env
    // proxy: {
    //   '/api': {
    //   target: 'http://localhost:5000',
    //   changeOrigin: true,
    //   secure: false,
    //   }
    // }
  }
})

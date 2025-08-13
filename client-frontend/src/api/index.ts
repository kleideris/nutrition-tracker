// src/api/index.ts
import { fetchWithAuth } from "./fetchWithAuth";

export const api = {
  post: (url: string, data: any) =>
    fetchWithAuth(url, {
      method: "POST",
      body: JSON.stringify(data),
    }),

  get: (url: string) =>
    fetchWithAuth(url, {
      method: "GET",
    }),

  delete: (url: string) =>
    fetchWithAuth(url, {
      method: "DELETE",
    }),
};
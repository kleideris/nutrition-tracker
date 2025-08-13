// src/api/fetchWithAuth.ts
import Cookies from "js-cookie";

const BASE_URL = import.meta.env.VITE_API_URL;

export async function fetchWithAuth(
  endpoint: string,
  options: RequestInit = {}
): Promise<Response> {
  const token = Cookies.get("authToken");

  const headers = {
    ...options.headers,
    Authorization: token ? `Bearer ${token}` : "",
    "Content-Type": "application/json",
  };

  const url = `${BASE_URL}${endpoint.startsWith("/") ? endpoint : `/${endpoint}`}`;

  return fetch(url, {
    ...options,
    headers,
  });
}
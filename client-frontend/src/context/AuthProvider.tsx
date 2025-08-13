import { useEffect, useState, type ReactNode } from "react"
import { deleteCookie, getCookie, setCookie } from "../utils/cookies"
import { jwtDecode } from "jwt-decode";
import {login, type LoginFields } from "../api/login";
import { AuthContext } from "./AuthContext";

type JwtPayload = {
  email?: string;
  user_id?: string;
};

export const AuthProvider = ({children}: {children: ReactNode}) => {
  const [accessToken, setAccessToken] = useState<string | null>(null);
  const [userId, setUserId] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const token = getCookie("access_token");
    setAccessToken(token ?? null);

    if (token) {
      try {
        const decoded = jwtDecode<JwtPayload>(token)
        console.log(decoded);  // <--- testing if decoded was successfull
        setUserId(decoded.user_id ?? null)

      } catch {
        setUserId(null);
      } 
    } else {
        setUserId(null);
    }
    setLoading(false);
  }, [])

  const loginUser = async (fields: LoginFields) => {
    const res = await login(fields);
    // console.log("Login response:", res);  // <--- testing login response in the console
    setCookie("access_token", res.access_token, {
      expires: 1,
      sameSite: "Lax",
      secure: false,  // We set it false in development otherwise it needs to be true
      path: "/"
    });

    setAccessToken(res.access_token);

    try {
      const decoded = jwtDecode<JwtPayload>(res.access_token);
      setUserId(decoded.user_id ?? null);
    } catch {
      setUserId(null);
    }
  };

  const logoutUser = () => {
    deleteCookie("access_token");
    setAccessToken(null);
    setUserId(null);
  };

  return(
    <AuthContext.Provider
    value = {{
      isAuthenticated: !!accessToken,
      accessToken,
      userId,
      loginUser,
      logoutUser,
      loading
    }}
    >
      {loading ? null : children}
    </AuthContext.Provider>
  )
}





















// import { useState } from 'react';
// import type { ReactNode } from 'react';
// import { AuthContext } from './AuthContext';
// import type { User} from './AuthTypes';

// export const AuthProvider = ({ children }: { children: ReactNode }) => {
//   const [user, setUser] = useState<User | null>(null);

//   const login = (token: string, userData: User) => {
//     localStorage.setItem('token', token);
//     setUser(userData);
//   };

//   const logout = () => {
//     localStorage.removeItem('token');
//     setUser(null);
//   };

//   return (
//     <AuthContext.Provider value={{ user, login, logout }}>
//       {children}
//     </AuthContext.Provider>
//   );
// };
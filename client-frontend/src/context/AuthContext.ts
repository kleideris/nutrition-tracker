import { createContext } from "react";
import type { LoginFields } from "../api/login";
import type { User } from "./AuthProvider";

// type User = {
//   id: number;
//   usename: string;
//   email: string;
//   userRole: string;
// }

type AuthContextProps = {
  isAuthenticated: boolean;
  accessToken: string | null;
  user: User | null;
  loginUser: (fields: LoginFields) => Promise<void>;
  logoutUser: () => void;
  loading: boolean;
};

export const AuthContext = createContext<AuthContextProps | undefined>(undefined);
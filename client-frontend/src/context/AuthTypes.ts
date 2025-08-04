export interface User {
  id: number;
  username: string;
  email: string;
  userRole: string;
}

export interface AuthContextType {
  user: User | null;
  login: (token: string, userData: User) => void;
  logout: () => void;
}
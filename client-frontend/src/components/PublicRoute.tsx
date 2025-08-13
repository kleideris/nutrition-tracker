import { Navigate } from "react-router-dom";
import { useAuth } from "@/hooks/useAuth";

export function PublicRoute({ children }: { children: React.ReactElement }) {
  const { isAuthenticated } = useAuth();

  return isAuthenticated ? <Navigate to="/dashboard" /> : children;
}
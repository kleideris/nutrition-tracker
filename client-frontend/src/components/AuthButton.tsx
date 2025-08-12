import { useAuth } from "@/hooks/useAuth";
import { Button } from "@/components/ui/button";
import { useNavigate } from "react-router";
import { toast } from "sonner";

export function AuthButton() {
  const { isAuthenticated, logoutUser } = useAuth();
  const navigate = useNavigate();

  const handleLogin = () => {
    navigate("/login");
  };

  const handleLogout = () => {
    logoutUser();
    toast.success("Logged out!");
    navigate("/login");
  };

  return isAuthenticated ? (
    <Button variant="default" onClick={handleLogout}>
      Logout
    </Button>
  ) : (
    <Button variant="secondary" onClick={handleLogin}>
      Login
    </Button>
  );
}

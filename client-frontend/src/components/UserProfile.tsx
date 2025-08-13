import { useEffect, useState } from "react";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import BackButton from "@/components/BackButton";
import { toast } from "sonner";
import { useAuth } from "@/hooks/useAuth";

export default function UserProfile() {
  const { user: authUser, isAuthenticated, loading } = useAuth();
  const [userDetails, setUserDetails] = useState<any>(null);

  useEffect(() => {
    if (!authUser?.id || !isAuthenticated || loading) return;

    const fetchUser = async () => {
      try {
        const res = await fetchWithAuth(`/users/${authUser.id}`);
        if (!res.ok) throw new Error("Failed to fetch user");
        const data = await res.json();
        setUserDetails(data);
      } catch {
        toast.error("Unable to load profile.");
      }
    };

    fetchUser();
  }, [authUser, isAuthenticated, loading]);

  return (
    <div className="max-w-xl mx-auto bg-white p-8 rounded-lg shadow">
      <BackButton />
      <h2 className="text-2xl font-semibold mb-6 border-b pb-2">👤 Profile Information</h2>

      {userDetails ? (
        <div className="space-y-4">
          <div className="flex justify-between border-b pb-2">
            <span className="font-medium text-gray-700">Username:</span>
            <span>{userDetails.username}</span>
          </div>
          <div className="flex justify-between border-b pb-2">
            <span className="font-medium text-gray-700">Email:</span>
            <span>{userDetails.email}</span>
          </div>
          <div className="flex justify-between border-b pb-2">
            <span className="font-medium text-gray-700">Role:</span>
            <span>{userDetails.userRole}</span>
          </div>
        </div>
      ) : (
        <p className="text-gray-500">Loading profile...</p>
      )}
    </div>
  );
}
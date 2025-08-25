import { fetchWithAuth } from "@/api/fetchWithAuth";
import type { UpdateFields } from "@/schemas/userSchema";
import type { RawUser, User } from "@/types/user";
import { useState } from "react";
import { toast } from "sonner";

export function useUsers(page: number, pageSize: number) {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);

  const fetchUsers = async () => {
    setLoading(true);
    try {
      const res = await fetchWithAuth(`/users?pageNumber=${page}&pageSize=${pageSize}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
      });

      if (!res.ok) throw new Error("Failed to fetch users");

      const data = await res.json();
      setUsers((data.items || data).map((u: RawUser) => ({
        ...u,
        role: u.userRole,
      })));
    } catch {
      toast.error("Error loading users");
    } finally {
      setLoading(false);
    }
  };

  const isLastAdmin = async (userId: number): Promise<boolean> => {
    const res = await fetchWithAuth("/users/admin-count", {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
    });

    if (!res.ok) return false;

    const count = await res.json();
    return count === 1 && users.find(u => u.id === userId)?.role === "Admin";
  };

  const updateUser = async (userId: number, data: UpdateFields) => {
    const { role, ...userData } = data;

    try {
      const userRes = await fetchWithAuth(`/users/${userId}`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
        body: JSON.stringify(userData),
      });

      if (!userRes.ok) throw new Error("Failed to update user");

      if (role === "User" && await isLastAdmin(userId)) {
        toast.error("Cannot demote the last admin");
        return;
      }

      const roleRes = await fetchWithAuth(`/users/${userId}/role`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
        body: JSON.stringify({ newRole: role }),
      });

      if (!roleRes.ok) throw new Error("Failed to update role");

      toast.success("User updated");
      fetchUsers();
    } catch {
      toast.error("Error updating user");
    }
  };

  const deleteUser = async (id: number) => {
    if (!confirm("Are you sure you want to delete this user?")) return;

    if (await isLastAdmin(id)) {
      toast.error("Cannot delete the last admin");
      return;
    }

    try {
      const res = await fetchWithAuth(`/users/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
      });

      if (!res.ok) throw new Error("Failed to delete user");

      toast.success("User deleted");
      fetchUsers();
    } catch {
      toast.error("Error deleting user");
    }
  };

  return {
    users,
    loading,
    fetchUsers,
    updateUser,
    deleteUser,
  };
}
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { useEffect, useState } from "react";
import { toast } from "sonner";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";

const UpdateSchema = z.object({
  username: z.string().min(2).max(50),
  email: z.string().email().optional(),
  firstname: z.string().min(2).max(50),
  lastname: z.string().min(2).max(50),
  role: z.enum(["Admin", "User"]),
});

type UpdateFields = z.infer<typeof UpdateSchema>;

type User = {
  id: number;
  username: string;
  email: string;
  firstname: string;
  lastname: string;
  role: "Admin" | "User";
};

type RawUser = Omit<User, "role"> & { userRole: "Admin" | "User" };

export default function UsersPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(10);
  const [loading, setLoading] = useState(false);
  const [editUserId, setEditUserId] = useState<number | null>(null);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<UpdateFields>({
    resolver: zodResolver(UpdateSchema),
  });

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
        role: u.userRole, // Normalize userRole → role
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
    // console.log("Admin count:", count);

    return count === 1 && users.find(u => u.id === userId)?.role === "Admin";
  };

  const updateUser = async (data: UpdateFields) => {
    if (!editUserId) return;

    const { role, ...userData } = data;



    try {
      // First updates user data
      const userRes = await fetchWithAuth(`/users/${editUserId}`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
        body: JSON.stringify(userData),
      });

      if (!userRes.ok) throw new Error("Failed to update user");

      // Then updates role

      // Prevent demoting the last admin
      if (role === "User" && await isLastAdmin(editUserId)) {
        toast.error("Cannot demote the last admin");
        return;
      }
      const roleRes = await fetchWithAuth(`/users/${editUserId}/role`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
        body: JSON.stringify({ newRole: role }),
      });

      if (!roleRes.ok) throw new Error("Failed to update role");

      // Only shows success if both succeeded
      toast.success("User updated");
      setEditUserId(null);
      fetchUsers();
    } catch (err: any) {
      toast.error(err.message || "Error updating user");
    }
  };

  const deleteUser = async (id: number) => {
    if (!confirm("Are you sure you want to delete this user?")) return;

    // Prevent deleting the last admin
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

  useEffect(() => {
    fetchUsers();
  }, [page]);

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">User Management</h1>

      {loading ? (
        <p>Loading users...</p>
      ) : (
        <div className="overflow-x-auto w-full">
          <table className="min-w-full table-fixed border">
            <thead>
              <tr className="bg-gray-200 text-left">
                <th className="p-2 w-[150px]">ID</th>
                <th className="p-2 w-[150px]">Username</th>
                <th className="p-2 w-[150px]">Email</th>
                <th className="p-2 w-[150px]">Firstname</th>
                <th className="p-2 w-[150px]">Lastname</th>
                <th className="p-2 border-r w-[150px]">Role</th>
                <th className="p-2 w-[150px]">Actions</th>
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.id} className="border-t text-left">
                  <td className="p-2 truncate whitespace-nowrap max-w-[150px]">
                    {user.id}
                  </td>
                  <td className="p-2 truncate whitespace-nowrap max-w-[150px]" title={user.username}>
                    {user.username}
                  </td>
                  <td className="p-2 truncate whitespace-nowrap max-w-[150px]" title={user.email}>
                    {user.email}
                  </td>
                  <td className="p-2 truncate whitespace-nowrap max-w-[150px]" title={user.firstname}>
                    {user.firstname}
                  </td>
                  <td className="p-2 truncate whitespace-nowrap max-w-[150px]" title={user.lastname}>
                    {user.lastname}
                  </td>
                  <td className="p-2 border-r">
                    {user.role}
                  </td>
                  <td className="p-2 truncate whitespace-nowrap max-w-[150px]">
                    <button
                      className="text-blue-600 hover:underline mr-2"
                      onClick={() => {
                        setEditUserId(user.id);
                        reset({
                          username: user.username,
                          email: user.email,
                          firstname: user.firstname,
                          lastname: user.lastname,
                          role: user.role as "Admin" | "User",
                        });
                      }}
                    >
                      Edit
                    </button>
                    <button
                      className="text-red-600 hover:underline"
                      onClick={() => deleteUser(user.id)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {editUserId !== null && (
        <form onSubmit={handleSubmit(updateUser)} className="mt-6 space-y-4 max-w-md">
          <h2 className="text-xl font-semibold">Edit User</h2>
          <div className="grid grid-cols-2 gap-2">
            <div>
              <label className="block text-sm font-medium">Username</label>
              <input type="text" {...register("username")} className="border px-2 py-1 w-full" disabled={isSubmitting} />
              {errors.username && <p className="text-red-600 text-sm">{errors.username.message}</p>}
            </div>
            <div>
              <label className="block text-sm font-medium">Email</label>
              <input type="email" {...register("email")} className="border px-2 py-1 w-full" disabled={isSubmitting} />
              {errors.email && <p className="text-red-600 text-sm">{errors.email.message}</p>}
            </div>
            <div>
              <label className="block text-sm font-medium">Firstname</label>
              <input type="text" {...register("firstname")} className="border px-2 py-1 w-full" disabled={isSubmitting} />
              {errors.firstname && <p className="text-red-600 text-sm">{errors.firstname.message}</p>}
            </div>
            <div>
              <label className="block text-sm font-medium">Lastname</label>
              <input type="text" {...register("lastname")} className="border px-2 py-1 w-full" disabled={isSubmitting} />
              {errors.lastname && <p className="text-red-600 text-sm">{errors.lastname.message}</p>}
            </div>
            <div>
              <label className="block text-sm font-medium">Role</label>
              <select {...register("role")} className="border px-2 py-1 w-full" disabled={isSubmitting}>
                <option value="User">User</option>
                <option value="Admin">Admin</option>
              </select>
              {errors.role && <p className="text-red-600 text-sm">{errors.role.message}</p>}
            </div>
            
          </div>
          <div className="flex gap-2">
            <button type="submit" className="bg-green-500 text-white px-4 py-1 rounded hover:bg-green-600" disabled={isSubmitting}>
              {isSubmitting ? "Updating..." : "Update User"}
            </button>
            <button type="button" className="bg-gray-300 px-4 py-1 rounded hover:bg-gray-400" onClick={() => setEditUserId(null)}>
              Cancel
            </button>
          </div>
        </form>
      )}

      <div className="mt-6 flex gap-4 items-center">
        {page > 1 && (
          <button className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400" onClick={() => setPage((p) => Math.max(p - 1, 1))}>
            Previous
          </button>
        )}
        {users.length === pageSize && (
          <div className="flex items-center gap-4">
            <span>Page {page}</span>
            <button
              className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
              onClick={() => setPage((p) => p + 1)}
            >
              Next
            </button>
          </div>
        )}
      </div>
    </div>
  );
}
         
import { useState, useEffect } from "react";
import { UsersTable } from "@/components/User/UsersTable";
import { useUsers } from "@/components/User/useUsers";
import type { UpdateFields } from "@/schemas/userSchema";
import { UserForm } from "@/components/User/UsersForm";
import type { User } from "@/types/user";

export default function UsersPage() {
  const [page, setPage] = useState(1);
  const pageSize = 10;
  const [editUserId, setEditUserId] = useState<number | null>(null);

  const {
    users,
    loading,
    fetchUsers,
    updateUser,
    deleteUser,
  } = useUsers(page, pageSize);

  useEffect(() => {
    fetchUsers();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page]);

  const handleEdit = (user: User) => {
    setEditUserId(user.id);
  };


  const handleUpdate = async (data: UpdateFields) => {
    if (editUserId === null) return;
    await updateUser(editUserId, data);
    setEditUserId(null);
  };

  const handleCancel = () => {
    setEditUserId(null);
  };

  const editUser = users.find((u) => u.id === editUserId) ?? undefined;

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">User Management</h1>

      {loading ? (
        <p>Loading users...</p>
      ) : (
        <UsersTable users={users} onEdit={handleEdit} onDelete={deleteUser} />
      )}

      {editUserId !== null && editUser && (
        <UserForm
          defaultValues={editUser}
          onSubmit={handleUpdate}
          onCancel={handleCancel}
          isSubmitting={false} // You can wire this to a local state if needed
        />
      )}

      <div className="mt-6 flex gap-4 items-center">
        {page > 1 && (
          <button
            className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
            onClick={() => setPage((p) => Math.max(p - 1, 1))}
          >
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
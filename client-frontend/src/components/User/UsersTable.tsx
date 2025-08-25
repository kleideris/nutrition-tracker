import type { Props } from "@/types/user";

export function UsersTable({ users, onEdit, onDelete }: Props) {
  return (
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
              <td className="p-2 truncate max-w-[150px]">{user.id}</td>
              <td className="p-2 truncate max-w-[150px]" title={user.username}>{user.username}</td>
              <td className="p-2 truncate max-w-[150px]" title={user.email}>{user.email}</td>
              <td className="p-2 truncate max-w-[150px]" title={user.firstname}>{user.firstname}</td>
              <td className="p-2 truncate max-w-[150px]" title={user.lastname}>{user.lastname}</td>
              <td className="p-2 border-r">{user.role}</td>
              <td className="p-2 truncate max-w-[150px]">
                <button
                  className="text-blue-600 hover:underline mr-2"
                  onClick={() => onEdit(user)}
                >
                  Edit
                </button>
                <button
                  className="text-red-600 hover:underline"
                  onClick={() => onDelete(user.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
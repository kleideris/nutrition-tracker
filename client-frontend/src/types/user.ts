export type User = {
  id: number;
  username: string;
  email: string;
  firstname: string;
  lastname: string;
  role: "Admin" | "User";
};

export type RawUser = Omit<User, "role"> & { userRole: "Admin" | "User" };

export type Props = {
  users: User[];
  onEdit: (user: User) => void;
  onDelete: (id: number) => void;
};
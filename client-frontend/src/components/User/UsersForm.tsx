import { UpdateSchema } from "@/schemas/userSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import z from "zod";



type UpdateFields = z.infer<typeof UpdateSchema>;

type Props = {
  defaultValues: UpdateFields;
  onSubmit: (data: UpdateFields) => void;
  onCancel: () => void;
  isSubmitting: boolean;
};


export function UserForm({ defaultValues, onSubmit, onCancel, isSubmitting }: Props) {
  const { register, handleSubmit, formState: { errors } } = useForm<UpdateFields>({
    resolver: zodResolver(UpdateSchema),
    defaultValues,
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="mt-6 space-y-4 max-w-md">
      <h2 className="text-xl font-semibold">Edit User</h2>
      <div className="grid grid-cols-2 gap-2">
        {/* Form fields */}
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
        <button type="button" className="bg-gray-300 px-4 py-1 rounded hover:bg-gray-400" onClick={onCancel}>
          Cancel
        </button>
      </div>
    </form>
  );
}
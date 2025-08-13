import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { toast } from "sonner";
import { useNavigate } from "react-router-dom";
import { z } from "zod";

const passwordRegex =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W).{8,}$/;

const RegisterSchema = z
  .object({
    username: z.string().min(2).max(50, "Username must be between 2 and 50 characters"),
    password: z.string().regex(passwordRegex, {
      message:
        "Password must contain uppercase, lowercase, digit, special character, and be at least 8 characters"
    }),
    confirmPassword: z.string(),
    email: z.string().email("Invalid email format").optional(),
    firstname: z.string().min(2).max(50, "Firstname must be between 2 and 50 characters"),
    lastname: z.string().min(2).max(50, "Lastname must be between 2 and 50 characters")
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ["confirmPassword"],
    message: "Passwords do not match"
  });

type RegisterFields = z.infer<typeof RegisterSchema>;

export default function RegisterPage() {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting }
  } = useForm<RegisterFields>({
    resolver: zodResolver(RegisterSchema)
  });

  const onSubmit = async (data: RegisterFields) => {
    try {
      const res = await fetch(import.meta.env.VITE_API_URL + "/users", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
      });

      if (!res.ok) {
        const error = await res.json();
        throw new Error(error?.message || "Registration failed");
      }

      toast.success("Account created successfully");
      navigate("/login", { replace: true });
    } catch (err) {
      toast.error(err instanceof Error ? err.message : "Registration failed");
    }
  };

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="max-w-sm mx-auto p-8 space-y-4 border rounded"
    >
      <h1 className="text-xl font-semibold mb-4">Create Account</h1>

      <div>
        <Label className="py-1" htmlFor="username">Username</Label>
        <Input id="username" {...register("username")} disabled={isSubmitting} />
        {errors.username && <p className="text-red-600 text-sm">{errors.username.message}</p>}
      </div>

      <div>
        <Label className="py-1" htmlFor="password">Password</Label>
        <Input id="password" type="password" {...register("password")} disabled={isSubmitting} />
        {errors.password && <p className="text-red-600 text-sm">{errors.password.message}</p>}
      </div>

      <div>
        <Label className="py-1" htmlFor="confirmPassword">Confirm Password</Label>
        <Input id="confirmPassword" type="password" {...register("confirmPassword")} disabled={isSubmitting} />
        {errors.confirmPassword && <p className="text-red-600 text-sm">{errors.confirmPassword.message}</p>}
      </div>

      <div>
        <Label className="py-1" htmlFor="email">Email (optional)</Label>
        <Input id="email" type="email" {...register("email")} disabled={isSubmitting} />
        {errors.email && <p className="text-red-600 text-sm">{errors.email.message}</p>}
      </div>

      <div>
        <Label className="py-1" htmlFor="firstname">Firstname</Label>
        <Input id="firstname" {...register("firstname")} disabled={isSubmitting} />
        {errors.firstname && <p className="text-red-600 text-sm">{errors.firstname.message}</p>}
      </div>

      <div>
        <Label className="py-1" htmlFor="lastname">Lastname</Label>
        <Input id="lastname" {...register("lastname")} disabled={isSubmitting} />
        {errors.lastname && <p className="text-red-600 text-sm">{errors.lastname.message}</p>}
      </div>

      <Button type="submit" disabled={isSubmitting} className="w-full">
        {isSubmitting ? "Creating..." : "Create Account"}
      </Button>
    </form>
  );
}
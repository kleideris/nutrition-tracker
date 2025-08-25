import z from "zod";

export const UpdateSchema = z.object({
  username: z.string().min(2).max(50),
  email: z.email().optional(),
  firstname: z.string().min(2).max(50),
  lastname: z.string().min(2).max(50),
  role: z.enum(["Admin", "User"]),
});

export type UpdateFields = z.infer<typeof UpdateSchema>;
import { z } from "zod"

export const LoginSchema = z.object({
  username: z.string().min(1, "Username is required"),
  password: z.string().min(1, "Password is required"),
})

export type LoginFields = z.infer<typeof LoginSchema>

// export type LoginFields = {
//   username: string;
//   password: string;
// };

export type LoginResponse = {
  access_token: string;
  token_type: string;
};

export async function login({username, password}: LoginFields): Promise<LoginResponse> {

  const res = await fetch(
    import.meta.env.VITE_API_URL + "/auth/login/access-token",
    {
      method: "POST",
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify({ username, password }),
    }
  );

  if (!res.ok) {
    let detail = "Login Failed"
    try {
      const data = await res.json();
      if (typeof data?.detail == "string") detail = data.detail;
    } catch (error) {
      console.error(error);
    }
    throw new Error(detail)
  }
  return await res.json();
}
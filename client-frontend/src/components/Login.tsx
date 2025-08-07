import { useState, useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

interface LoginForm {
  username: string;
  password: string;
}



interface DecodedToken {
  name?: string;
  username?: string;
  email?: string;
  // Add other fields as needed based on your token structure
}


const Login = () => {
  const auth = useContext(AuthContext);
  const [form, setForm] = useState<LoginForm>({ username: '', password: '' });
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const payload = {
    UsernameOrEmail: form.username,
    Password: form.password,
    };

    const apiUrl = import.meta.env.VITE_API_URL;

    try {
      const res = await fetch(`${apiUrl}/api/auth/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
      });

      if (!res.ok) throw new Error('Login failed');

      const data = await res.json();
      console.log('Received token:', data.token); // 👈 This will show the token in the browser console

      const decoded: DecodedToken = jwtDecode(data.token);
      console.log('Decoded token:', decoded);

      auth?.login(data.token, data.user); // You could also pass decoded.name or decoded.username if needed
      navigate("/dashboard");

    } catch (err) {
      alert('Login failed');
    }
  };


  return (
  <>
    <div className="min-h-screen flex items-center justify-center bg-gray-900">
      <form
        onSubmit={handleSubmit}
        className="bg-gray-800 p-8 rounded-lg shadow-md w-full max-w-sm flex flex-col gap-4"
      >
        <input
          type="text"
          placeholder="Username"
          value={form.username}
          onChange={(e) => setForm({ ...form, username: e.target.value })}
          className="px-4 py-2 rounded bg-gray-700 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-green-500"
        />
        <input
          type="password"
          placeholder="Password"
          value={form.password}
          onChange={(e) => setForm({ ...form, password: e.target.value })}
          className="px-4 py-2 rounded bg-gray-700 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-green-500"
        />
        <button
          type="submit"
          className="bg-green-600 hover:bg-green-700 text-white font-semibold py-2 rounded"
        >
          Login
        </button>
      </form>
    </div>
  </>
  );
};

export default Login;
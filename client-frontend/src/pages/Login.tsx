import { useState, useContext } from 'react';
import { AuthContext } from '../context/AuthContext';

interface LoginForm {
  username: string;
  password: string;
}

const Login = () => {
  const auth = useContext(AuthContext);
  const [form, setForm] = useState<LoginForm>({ username: '', password: '' });

const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();

    const payload = {
    UsernameOrEmail: form.username,
    Password: form.password,
  };

const apiUrl = import.meta.env.VITE_API_URL;

console.log("API URL:", apiUrl)  //Logginf for test

  try {
    const res = await fetch(`${apiUrl}/api/User/LoginUser`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload),
    });

    console.log('Sending:', payload);  //Logging for test

    if (!res.ok) throw new Error('Login failed');

    const data = await res.json();

    console.log('Received token:', data.token); // 👈 This will show the token in your browser console

    auth?.login(data.token, data.user);
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
          className="px-4 py-2 rounded bg-gray-700 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="password"
          placeholder="Password"
          value={form.password}
          onChange={(e) => setForm({ ...form, password: e.target.value })}
          className="px-4 py-2 rounded bg-gray-700 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <button
          type="submit"
          className="bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 rounded"
        >
          Login
        </button>
      </form>
    </div>
  </>
  );
};

export default Login;
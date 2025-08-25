// src/components/Navbar.tsx
import { useAuth } from "@/hooks/useAuth";
import { Link } from "react-router-dom";

export default function Navbar() {
  const { user } = useAuth();
  const isAdmin = user?.role === "Admin"

  return (
    <aside className="w-64 bg-gray-800 text-white p-6 space-y-6 min-h-screen">
      <h2 className="text-2xl font-bold text-center">Nutrition Tracker</h2>
      <nav className="flex flex-col space-y-4 mt-6">
        <Link
          to="/dashboard/log-meal"
          className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition"
        >
          🍽️ Log Meal
        </Link>
        <Link
          to="/dashboard/my-meals"
          className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition"
        >
          📋 My Meals
        </Link>
        <Link
          to="/dashboard/food-items"
          className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition"
        >
          🍎 Food Items
        </Link>
        <Link
          to="/dashboard/profile"
          className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition"
        >
          👤 Profile
        </Link>
        {isAdmin && (
          <Link
            to="/dashboard/users"
            className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition"
          >
            🧑‍💼 Users
          </Link>
        )}
      </nav>
    </aside>
  );
}
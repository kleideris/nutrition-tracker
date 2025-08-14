// src/pages/Dashboard.tsx
import { Outlet, Link } from "react-router-dom";

export default function Dashboard() {
  return (
    <div className="flex min-h-screen font-sans bg-gray-100">
      <aside className="w-64 bg-gray-800 text-white p-6 space-y-6">
        <h2 className="text-2xl font-bold text-center">Nutrition Tracker</h2>
        <nav className="flex flex-col space-y-4">
          <Link to="log-meal" className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition">🍽️ Log Meal</Link>
          <Link to="my-meals" className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition">📋 Meals</Link>
          <Link to="food-items" className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition">🍎 Food Items</Link>
          <Link to="profile" className="bg-gray-700 px-4 py-2 rounded hover:bg-gray-600 transition">👤 Profile</Link>
        </nav>
      </aside>
      <main className="flex-1 p-8">
        <Outlet />
      </main>
    </div>
  );
}
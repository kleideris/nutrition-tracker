// src/components/Navbar.tsx
import { useAuth } from "@/hooks/useAuth";
import { Link, useLocation } from "react-router-dom";
import {
  FaUtensils,
  FaClipboardList,
  FaAppleAlt,
  FaUser,
  FaUsers,
} from "react-icons/fa";

export default function Navbar() {
  const { user } = useAuth();
  const isAdmin = user?.role === "Admin";
  const location = useLocation();

  const navItems = [
    { to: "/dashboard/log-meal", label: "Log Meal", icon: <FaUtensils className="text-white"/> },
    { to: "/dashboard/my-meals", label: "My Meals", icon: <FaClipboardList className="text-green-400" /> },
    { to: "/dashboard/food-items", label: "Food Items", icon: <FaAppleAlt className="text-red-400"/> },
    { to: "/dashboard/profile", label: "Profile", icon: <FaUser className="text-blue-400" /> },
  ];

  if (isAdmin) {
    navItems.push({
      to: "/dashboard/users",
      label: "Users",
      icon: <FaUsers className="text-cyan-400" />,
    });
  }

  return (
    <aside className="w-64 bg-gray-800 text-white p-6 space-y-6 min-h-screen">
      <h2 className="text-2xl font-bold text-center">Nutrition Tracker</h2>
      <nav className="flex flex-col space-y-4 mt-6">
        {navItems.map(({ to, label, icon }) => {
          const isActive = location.pathname === to;
          return (
            <Link
              key={to}
              to={to}
              className={`px-4 py-2 rounded transition flex items-center space-x-3 ${
                isActive
                  ? "bg-green-600"
                  : "bg-gray-700 hover:bg-gray-600"
              }`}
            >
              <span className="text-lg">{icon}</span>
              <span>{label}</span>
            </Link>
          );
        })}
      </nav>
    </aside>
  );
}
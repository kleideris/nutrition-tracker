import { Outlet } from "react-router-dom";
import Navbar from "../components/Navbar";

export default function DashboardPage() {
  return (
      <div className="flex min-h-screen font-sans">
        <Navbar />
        <main className="flex-1 p-8">
          <Outlet />
        </main>
      </div>
  );
}
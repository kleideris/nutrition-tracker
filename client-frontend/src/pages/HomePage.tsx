import { Link } from "react-router-dom";

export default function HomePage() {
  return (
      <div className="flex justify-center text-center pt-24 px-6">
        <div className="bg-opacity-50 rounded-lg p-8">
          <h1 className="text-4xl md:text-5xl font-bold text-white mb-4 drop-shadow-[0_0_4px_black]">
            Welcome to Nutrition Tracker
          </h1>
          <p className="text-white text-lg mb-6 max-w-xl mx-auto drop-shadow-[0_0_4px_black]">
            Your personal companion for tracking food, macros, and healthy habits.
          </p>
          <Link
            to="/dashboard"
            className="inline-block bg-green-500 text-white px-6 py-3 rounded-md hover:bg-green-600 transition-colors"
          >
            Get Started
          </Link>
        </div>
      </div>
  );
}
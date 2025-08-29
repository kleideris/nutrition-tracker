import { useNavigate } from "react-router-dom";

export default function BackButton({ label = "Back" }: { label?: string }) {
  const navigate = useNavigate();
  return (
    <button
      onClick={() => navigate(-1)}
      className="mb-4 px-4 py-2 bg-gray-100 border border-gray-300 rounded hover:bg-gray-200 transition"
    >
      ← {label}
    </button>
  );
}
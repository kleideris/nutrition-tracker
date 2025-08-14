import React from "react";

interface Props {
  value: string;
  onChange: (val: string) => void;
  onSearch: () => void;
}

export const SearchBar: React.FC<Props> = ({ value, onChange, onSearch }) => {
  return (
    <div className="flex flex-col sm:flex-row items-center gap-4 bg-white p-4 rounded-lg shadow-sm border border-gray-200">
      <input
        type="text"
        value={value}
        onChange={e => onChange(e.target.value)}
        placeholder="Search food items..."
        className="w-full sm:w-2/3 border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-green-400"
      />
      <button
        onClick={onSearch}
        className="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600 transition w-full sm:w-auto"
      >
        Search
      </button>
    </div>
  );
};
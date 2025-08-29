export const SearchBar = ({ query, setQuery, handleFocus }: {
  query: string;
  setQuery: (val: string) => void;
  handleFocus: () => void;
}) => (
  <div className="flex gap-4">
    <input
      value={query}
      onChange={(e) => setQuery(e.target.value)}
      onFocus={handleFocus}
      placeholder="Search food items..."
      className="flex-1 border rounded-md p-2 bg-white"
    />
  </div>
);
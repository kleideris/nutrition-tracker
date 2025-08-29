import type { FoodItemDto } from "@/types/foodItemDto";

export const SearchResults = ({
  results,
  loading,
  query,
  handleSelect,
}: {
  results: FoodItemDto[];
  loading: boolean;
  query: string;
  handleSelect: (item: FoodItemDto) => void;
}) => (
  <div className="space-y-2">
    {loading ? (
      <div className="animate-pulse italic">Searching…</div>
    ) : (
      <>
        {results.length === 0 && query.trim() !== "" && (
          <div className="italic">No food items found.</div>
        )}
        {results.map((item) => (
          <div
            key={item.id}
            onClick={() => handleSelect(item)}
            className="cursor-pointer bg-white p-4 rounded-md border hover:bg-gray-50"
          >
            <div className="font-semibold text-green-700">{item.name}</div>
          </div>
        ))}
      </>
    )}
  </div>
);
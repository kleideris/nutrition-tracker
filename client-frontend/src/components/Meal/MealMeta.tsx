export const MealMeta = ({
  mealType,
  setMealType,
  timestamp,
  setTimestamp,
  handleSubmit,
  mode,
}: {
  mealType: string;
  setMealType: (val: string) => void;
  timestamp: string;
  setTimestamp: (val: string) => void;
  handleSubmit: () => void;
  mode: "create" | "edit";
}) => {
  const mealTypes = ["Breakfast", "Lunch", "Dinner", "Snack"];

  return (
    <div className="flex justify-between items-center flex-wrap gap-4">
      <div className="flex gap-4 items-center flex-wrap">
        <label>Meal Type:</label>
        <select value={mealType} onChange={(e) => setMealType(e.target.value)} className="border rounded px-2 py-1">
          {mealTypes.map((type) => (
            <option key={type}>{type}</option>
          ))}
        </select>

        <label>Timestamp:</label>
        <input
          type="datetime-local"
          value={new Date(timestamp).toISOString().slice(0, 16)}
          onChange={(e) => setTimestamp(new Date(e.target.value).toISOString())}
          className="border rounded px-2 py-1"
        />
      </div>
      <button
        onClick={handleSubmit}
        className="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600"
      >
        {mode === "create" ? "Log Meal" : "Update Meal"}
      </button>
    </div>
  );
};
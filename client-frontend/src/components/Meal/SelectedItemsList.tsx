import type { SelectedItem } from "@/types/meals";

const measurementUnits = ["g", "serving"];

export const SelectedItemsList = ({
  selectedItems,
  updateItem,
  handleRemove,
}: {
  selectedItems: SelectedItem[];
  updateItem: <K extends keyof SelectedItem>(index: number, field: K, value: SelectedItem[K]) => void;
  handleRemove: (index: number) => void;
}) => (
  <div className="max-h-[300px] overflow-y-auto space-y-2 border rounded-md p-2 bg-gray-50">
    {selectedItems.map((item, index) => (
      <div key={item.id} className="flex items-center gap-4 bg-white p-4 rounded-md border">
        <div className="flex-1">
          <div className="font-medium">{item.name}</div>
        </div>
        <input
          type="number"
          value={item.quantity}
          onChange={(e) => updateItem(index, "quantity", Number(e.target.value))}
          className="w-20 border rounded px-2 py-1"
        />
        <select
          value={item.unit}
          onChange={(e) => updateItem(index, "unit", e.target.value)}
          className="border rounded px-2 py-1"
        >
          {measurementUnits.map((unit) => (
            <option key={unit}>{unit}</option>
          ))}
        </select>
        <button onClick={() => handleRemove(index)} className="text-red-500 text-sm hover:underline">
          Remove
        </button>
      </div>
    ))}
  </div>
);
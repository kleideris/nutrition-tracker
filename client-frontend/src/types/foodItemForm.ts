import type { FoodItemDto } from "./foodItemDto";

export type AddFoodItemProps = {
  onAdd: (item: FoodItemDto) => void;
};

export type FoodItemFormProps = {
  formData: FoodItemDto;
  setFormData: React.Dispatch<React.SetStateAction<FoodItemDto>>;
  onSubmit: (e: React.FormEvent) => void;
};

export type RemoveFootItemProps = {
  apiUrl: string;
};

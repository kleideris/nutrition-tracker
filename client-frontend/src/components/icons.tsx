import {
  FaEdit,
  FaTrashAlt,
  FaFireAlt,
  FaDrumstickBite,
  FaBreadSlice,
  FaCheese,
  FaUtensils
} from "react-icons/fa";

export const iconStyles = {
  gray: "w-3 h-3 text-gray-600",
  base: "w-5 h-5 text-gray-600 hover:text-gray-800",
  delete: "text-red-600 hover:text-red-800",
  edit: "text-blue-600 hover:text-blue-800",
};

type IconProps = React.ComponentProps<"svg">;

export const Icons = {

  Edit: (props: IconProps) => <FaEdit className={iconStyles.edit} {...props} />,
  Delete: (props: IconProps) => <FaTrashAlt className={iconStyles.delete} {...props} />,
  Calories: (props: IconProps) => <FaFireAlt className="text-orange-400" {...props} />,
  Protein: (props: IconProps) => <FaDrumstickBite className="text-red-500" {...props} />,
  Carbs: (props: IconProps) => <FaBreadSlice className="text-yellow-500" {...props} />,
  Fats: (props: IconProps) => <FaCheese className="text-blue-500" {...props} />,
  Serving: (props: IconProps) => <FaUtensils className="text-green-500" {...props} />
};
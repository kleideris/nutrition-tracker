using NutritionTracker.Api.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NutritionTracker.Api.Core.Helpers
{
    public static class FoodSeeder
    {
        public static async Task SeedAsync(AppDBContext context)
        {
            if (await context.FoodItems.AnyAsync()) return;

            var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "seed-food.json");
            if (!File.Exists(jsonPath)) return;

            var json = await File.ReadAllTextAsync(jsonPath);
            var foodItems = JsonSerializer.Deserialize<List<FoodItemSeed>>(json);

            if (foodItems != null)
            {
                foreach (var item in foodItems)
                {
                    var food = new FoodItem
                    {
                        Name = item.Name,
                        NutritionData = new NutritionData
                        {
                            Calories = item.NutritionData.Calories,
                            Fats = item.NutritionData.Fats,
                            Carbohydrates = item.NutritionData.Carbohydrates,
                            Protein = item.NutritionData.Protein,
                            ServingSizeGrams = item.NutritionData.ServingSizeGrams
                        }
                    };

                    context.FoodItems.Add(food);
                }

                await context.SaveChangesAsync();
            }
        }

        private class FoodItemSeed
        {
            public string Name { get; set; } = string.Empty;
            public NutritionSeed NutritionData { get; set; } = new NutritionSeed();
        }

        private class NutritionSeed
        {
            public float Calories { get; set; }
            public float Fats { get; set; }
            public float Carbohydrates { get; set; }
            public float Protein { get; set; }
            public double ServingSizeGrams { get; set; }
        }


    }
}

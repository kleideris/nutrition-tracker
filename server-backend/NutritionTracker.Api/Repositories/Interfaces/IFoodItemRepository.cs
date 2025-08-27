using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    /// <summary>
    /// Defines data access operations for managing FoodItem entities,
    /// including retrieval by name, listing all items, and performing search queries.
    /// </summary>
    public interface IFoodItemRepository
    {
        /// <summary>
        /// Retrieves a specific FoodItem entry based on its name. Returns null if no matching item is found.
        /// </summary>
        Task<FoodItem?> GetByNameAsync(string name);


        /// <summary>
        /// Retrieves all FoodItem entries from the database as a list.
        /// </summary>
        Task<List<FoodItem>> GetAllAsListAsync();


        /// <summary>
        /// Searches for FoodItem entries whose names match or contain the specified query string. Supports partial matches.
        /// </summary>
        Task<List<FoodItem>> SearchByNameAsync(string query);
    }
}

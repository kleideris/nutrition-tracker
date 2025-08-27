using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories.Interfaces;

namespace NutritionTracker.Api.Repositories
{
    /// <summary>
    /// Repository for performing data operations related to <see cref="FoodItem"/> entities.
    /// </summary>
    public class FoodItemRepository(AppDBContext context) : BaseRepository<FoodItem>(context), IFoodItemRepository
    {
        #region docs
        /// <summary>
        /// Retrieves a <see cref="FoodItem"/> by its ID, including its associated <see cref="NutritionData"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the food item.</param>
        /// <returns>
        /// A <see cref="FoodItem"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        #endregion
        public override async Task<FoodItem?> GetAsync(int id) => 
            await dbset.Include(f => f.NutritionData).FirstOrDefaultAsync(f => f.Id == id);


        #region docs
        /// <summary>
        /// Retrieves all <see cref="FoodItem"/> entities as a list, ordered by name.
        /// Includes associated <see cref="NutritionData"/>.
        /// </summary>
        /// <returns>A list of all <see cref="FoodItem"/> objects.</returns>
        #endregion
        public async Task<List<FoodItem>> GetAllAsListAsync() => 
            await dbset.Include(f => f.NutritionData)
                .OrderBy(f => f.Name)
                .ToListAsync();


        #region docs
        /// <summary>
        /// Retrieves a <see cref="FoodItem"/> by its name, including its associated <see cref="NutritionData"/>.
        /// </summary>
        /// <param name="name">The name of the food item.</param>
        /// <returns>
        /// A <see cref="FoodItem"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        #endregion
        public async Task<FoodItem?> GetByNameAsync(string name) => 
            await context.FoodItems.Include(f => f.NutritionData).FirstOrDefaultAsync(f => f.Name == name);


        #region docs
        /// <summary>
        /// Searches for <see cref="FoodItem"/> entities whose names contain the specified query string.
        /// Includes associated <see cref="NutritionData"/> and orders results by name.
        /// </summary>
        /// <param name="query">The substring to search for within food item names.</param>
        /// <returns>A list of matching <see cref="FoodItem"/> objects.</returns>
        # endregion
        public async Task<List<FoodItem>> SearchByNameAsync(string query) =>
            await context.FoodItems
                .Include(f => f.NutritionData)
                .Where(f => f.Name.Contains(query))
                .OrderBy(f => f.Name)
                .ToListAsync();
    }
}

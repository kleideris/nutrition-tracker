namespace NutritionTracker.Api.Repositories.Interfaces
{
    /// <summary>
    /// Defines a contract for coordinating multiple repository operations within a single transactional scope.
    /// Ensures consistency and atomicity when performing data operations across related entities.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository responsible for user-related data operations.
        /// </summary>
        UserRepository UserRepository { get; }

        /// <summary>
        /// Gets the repository responsible for meal-related data operations.
        /// </summary>
        MealRepository MealRepository { get; }

        /// <summary>
        /// Gets the repository responsible for managing relationships between meals and food items.
        /// </summary>
        MealFoodItemRepository MealFoodItemRepository { get; }

        /// <summary>
        /// Gets the repository responsible for food item-related data operations.
        /// </summary>
        FoodItemRepository FoodItemRepository { get; }

        /// <summary>
        /// Commits all changes made through the repositories in a single transaction. Returns true if the operation succeeds; otherwise, false.
        /// </summary>
        Task<bool> SaveAsync();
    }
}

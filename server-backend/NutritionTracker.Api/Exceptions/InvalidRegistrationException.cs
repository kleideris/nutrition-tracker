namespace NutritionTracker.Api.Exceptions
{
    public class InvalidRegistrationException : Exception
    {
        public InvalidRegistrationException(string? message) : base(message)
        {
        }
    }
}

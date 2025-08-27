namespace NutritionTracker.Api.Exceptions
{
    public class InvalidActionException : AppException
    {
        private static readonly string DEFAULT_CODE = "Invalid";

        public InvalidActionException(string code, string? message) : base(DEFAULT_CODE + code, message)
        {
        }
    }
}

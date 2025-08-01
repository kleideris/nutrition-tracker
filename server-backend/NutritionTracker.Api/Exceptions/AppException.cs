namespace NutritionTracker.Api.Exceptions
{
    public class AppException : Exception
    {
        public string Code { get; set; }

        public AppException(string code, string? message) : base(message)
        {
            Code = code;
        }
    }
}

namespace Raspberry.Aircon.Models
{
    public class ValidationResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }


        public ValidationResult(string message, bool success = false)
        {
            Message = message;
            Success = success;
        }
        public static ValidationResult Successful = new ValidationResult("Operation successfully completed.", true);
    }
}
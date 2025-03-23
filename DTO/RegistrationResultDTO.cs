using NUnit.Framework.Interfaces;

namespace TaskManagementAPI.DTO
{
    public class RegistrationResultDTO
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public static RegistrationResultDTO FailureResult(List<string> errors) => new()
        {
            Success = false,
            Errors = errors
        };
        public static RegistrationResultDTO SuccessResult() => new()
        {
            Success = true

        };

    }
}

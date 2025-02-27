using Utils.Responses;
using DTOs.Firebase;

namespace Utils.Validations
{
    public static class RegisterValidation
    {
        public static ResultResponse<RegisterDto> Validate(RegisterDto registerDto)
        {
            if (!InputValidator.IsValidEmail(registerDto.email))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "Invalid email format or email is too long.",
                    "EMAIL_INVALID"
                );
            }

            if (!InputValidator.IsValidName(registerDto.name))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "name must be at least 3 characters long.",
                    "NAME_TOO_SHORT"
                );
            }
            
            if (!InputValidator.IsValidName(registerDto.username))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "name must be at least 3 characters long.",
                    "NAME_TOO_SHORT"
                );
            }
            
            if (!InputValidator.IsValidPhoneNumber(registerDto.phone))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "phone number must be a valid phone number.",
                    "PHONE_INVALID"
                );
            }

            return ResultResponse<RegisterDto>.Success(registerDto, "Validation successful.");
        }
    }
}
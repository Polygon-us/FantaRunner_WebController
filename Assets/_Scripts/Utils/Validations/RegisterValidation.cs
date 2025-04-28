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
                    "El email es no es correcto.",
                    "EMAIL_INVALID"
                );
            }

            if (!InputValidator.IsValidName(registerDto.name))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "Nombre no debe contener caracteres especiales.",
                    "NAME_WITH_SPECIAL_CHARS"
                );
            }
            
            if (!InputValidator.IsValidUserName(registerDto.username))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "Nombre de usuario no debe contener espacios.",
                    "USERNAME_WHITESPACE"
                );
            }
            
            if (!InputValidator.IsValidPhoneNumber(registerDto.phone))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "El número de celular es incorrecto.",
                    "PHONE_INVALID"
                );
            }

            return ResultResponse<RegisterDto>.Success(registerDto, "Validation successful.");
        }
    }
}
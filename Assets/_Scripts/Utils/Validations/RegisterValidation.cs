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
                    "Nombre debe tener más de 3 caracteres.",
                    "NAME_TOO_SHORT"
                );
            }
            
            if (!InputValidator.IsValidName(registerDto.username))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "Usuario debe taner más de 3 caracteres.",
                    "NAME_TOO_SHORT"
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
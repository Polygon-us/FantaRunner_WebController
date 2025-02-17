using Source.Utils.Validations;
using Source.Utils.Responses;
using Source.DTOs.Request;
using UnityEngine.UI;
using UnityEngine;

public class RegisterController : MonoBehaviour
{
   [SerializeField] private CustomInputField nameInputField;
   [SerializeField] private CustomInputField usernameInputField;
   [SerializeField] private CustomInputField emailInputField;
   [SerializeField] private CustomInputField phoneInputField;
   [SerializeField] private Button sendButton;

   private void Awake()
   {
      sendButton.onClick.AddListener(SendRegister);
   }

   private void SendRegister()
   {
      RegisterDto registerDto = new RegisterDto
      {
         name = nameInputField.Text,
         username = usernameInputField.Text,
         email = emailInputField.Text,
         phone = phoneInputField.Text
      };

      ResultResponse<RegisterDto> validation = RegisterValidation.Validate(registerDto);

      if (!validation.IsSuccess)
      {
         Debug.Log(validation.ErrorMessage);
         return;
      }
      
      // TODO: Send to backend
   }
}

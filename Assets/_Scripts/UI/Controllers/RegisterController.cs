using Utils.Validations;
using Utils.Responses;
using UnityEngine.UI;
using UI.InputField;
using DTOs.Firebase;
using UnityEngine;
using System;

namespace UI.Controllers
{
    public class RegisterController : MonoBehaviour
    {
        [SerializeField] private CustomInputField nameInputField;
        [SerializeField] private CustomInputField usernameInputField;
        [SerializeField] private CustomInputField emailInputField;
        [SerializeField] private CustomInputField phoneInputField;
        [SerializeField] private Button sendButton;
        [SerializeField] private MockRegisterData mockRegisterData;
        
        public Action OnRegistered;

        private void Awake()
        {
            sendButton.onClick.AddListener(SendRegister);
        }

        private void Start()
        {
            if (!mockRegisterData)
                return;
            
            nameInputField.Text = mockRegisterData.RegisterMockData.name;
            usernameInputField.Text = mockRegisterData.RegisterMockData.username;
            emailInputField.Text = mockRegisterData.RegisterMockData.email;
            phoneInputField.Text = mockRegisterData.RegisterMockData.phone;
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

            FirestoreSender.Instance.SendUser(registerDto);
            
            // OnRegistered?.Invoke();
        }
    }
}
using FirebaseCore.Listeners;
using FirebaseCore.Senders;
using Utils.Validations;
using Utils.Responses;
using UnityEngine.UI;
using UI.InputField;
using DTOs.Firebase;
using UnityEngine;
using System;

namespace UI.Controllers
{
    public class RegisterController : ControllerBase
    {
        [SerializeField] private CustomInputField nameInputField;
        [SerializeField] private CustomInputField usernameInputField;
        [SerializeField] private CustomInputField emailInputField;
        [SerializeField] private CustomInputField phoneInputField;
        [SerializeField] private Button sendButton;
        [SerializeField] private MockRegisterData mockRegisterData;
        [SerializeField] private RoomConfig roomConfig;
        
        public Action OnRegistered;

        private UserSender userSender;
        private UserListener userListener;

        public override void OnCreation(UIController context)
        {
            base.OnCreation(context);
            
            sendButton.onClick.AddListener(SendRegister);
        }
        
        public override void OnShow()
        {
            if (mockRegisterData)
            {
                nameInputField.Text = mockRegisterData.RegisterMockData.name;
                usernameInputField.Text = mockRegisterData.RegisterMockData.username;
                emailInputField.Text = mockRegisterData.RegisterMockData.email;
                phoneInputField.Text = mockRegisterData.RegisterMockData.phone;
            }
            
            userSender = new UserSender(roomConfig.roomName);
            userListener = new UserListener(roomConfig.roomName);
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

            userSender.Send(registerDto);
            
            // OnRegistered?.Invoke();
        }

        public override void OnHide()
        {
            userListener.Disconnect();
        }
    }
}
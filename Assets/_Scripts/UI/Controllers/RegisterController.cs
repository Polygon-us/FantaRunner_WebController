using FirebaseCore.Listeners;
using FirebaseCore.Senders;
using Utils.Validations;
using FirebaseCore.DTOs;
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
        [SerializeField] private MessagePopup messagePopup;
        [SerializeField] private Button sendButton;
        [SerializeField] private MockRegisterData mockRegisterData;
        
        private UserSender userSender;
        private UserDatabaseSender userDatabaseSender;

        public override void OnCreation(RoomConfig roomConfig)
        {
            base.OnCreation(roomConfig);
            
            userSender = new UserSender(RoomConfig.roomName);
            userDatabaseSender = new UserDatabaseSender(RoomConfig.roomName);
            
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
            
            userSender.Delete();
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
                messagePopup.Show(validation.ErrorMessage);
                return;
            }

            userSender.Send(registerDto);
            userDatabaseSender.Send(registerDto);
            
            // OnRegistered?.Invoke();
        }

        public override void OnHide()
        {
            
        }
    }
}
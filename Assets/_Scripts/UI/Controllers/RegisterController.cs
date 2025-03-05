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
        [SerializeField] private Button sendButton;
        [SerializeField] private MockRegisterData mockRegisterData;
        
        public Action OnRegistered;

        private UserSender userSender;
        private UserListener userListener;
        private GameStateSender gameStateSender;

        public override void OnCreation(RoomConfig roomConfig)
        {
            base.OnCreation(roomConfig);
            
            userSender = new UserSender(RoomConfig.roomName);
            userListener = new UserListener(RoomConfig.roomName);
            gameStateSender = new GameStateSender(RoomConfig.roomName);
            
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
            
            userListener.OnDataReceived += OnUserReceived;
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

        private void OnUserReceived(UserDataDto _)
        {
            userListener.OnDataReceived -= OnUserReceived;

            GameStateDto gameStateDto = new GameStateDto
            {
                state = GameStates.Game
            };
            gameStateSender.Send(gameStateDto);
        }

        public override void OnHide()
        {
            userListener.Disconnect();
        }
    }
}